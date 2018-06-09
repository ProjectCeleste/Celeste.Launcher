#region Using directives

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class EditorForm : Form
    {
        private readonly GameScannnerApi _gameScannner;

        public EditorForm()
        {
            var path = !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath)
                ? Program.UserConfig.GameFilesPath
                : GameScannnerApi.GetGameFilesRootPath();

            _gameScannner = GameScannnerApi.InstallGameEditor(Program.UserConfig.IsSteamVersion,
                Program.UserConfig.IsLegacyXLive,
                path);

            InitializeComponent();

            SkinHelper.SetFont(Controls);
        }

        private async void EditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception)
            {
                //
            }

            if (DownloadFileUtils.IsConnectedToInternet())
            {
                if (await _gameScannner.QuickScan())
                {
                    Btn_Install_Editor.Enabled = false;
                    btn_Browse.Enabled = true;

                    label2.Text = @"Installed";
                    label2.ForeColor = Color.Green;
                }
                else
                {
                    label2.Text = @"Non-Installed Or Outdated";
                    label2.ForeColor = Color.Red;

                    Btn_Install_Editor.Enabled = true;
                    btn_Browse.Enabled = false;
                }
            }
            else
            {
                Btn_Install_Editor.Enabled = true;
                btn_Browse.Enabled = true;

                label2.Text = @"Unknow";
                label2.ForeColor = Color.OrangeRed;
            }
        }


        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            btn_Browse.Enabled = false;
            try
            {
                btn_Browse.Enabled = false;

                //Launch Game
                var path = !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath)
                    ? Program.UserConfig.GameFilesPath
                    : GameScannnerApi.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Editor.exe");

                if (!File.Exists(spartanPath))
                    throw new FileNotFoundException("Editor.exe not found!", spartanPath);

                //isSteam
                if (!Program.UserConfig.IsSteamVersion)
                {
                    var steamApiDll = Path.Combine(Program.UserConfig.GameFilesPath, "steam_api.dll");
                    if (File.Exists(steamApiDll))
                        File.Delete(steamApiDll);
                }

                //
                string lang;
                switch (Program.UserConfig.GameLanguage)
                {
                    case GameLanguage.deDE:
                        lang = "de-DE";
                        break;
                    case GameLanguage.enUS:
                        lang = "en-US";
                        break;
                    case GameLanguage.esES:
                        lang = "es-ES";
                        break;
                    case GameLanguage.frFR:
                        lang = "fr-FR";
                        break;
                    case GameLanguage.itIT:
                        lang = "it-IT";
                        break;
                    case GameLanguage.zhCHT:
                        lang = "zh-CHT";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Program.UserConfig.GameLanguage),
                            Program.UserConfig.GameLanguage, null);
                }

                //SymLink Profile Folder
                var profileDir = Path.Combine(Environment.GetEnvironmentVariable("userprofile"));
                var path1 = Path.Combine(profileDir, "Documents", "Age of Empires Online");
                var path2 = Path.Combine(profileDir, "Documents", "Spartan");

                if (Directory.Exists(path1) &&
                    (!Misc.IsSymLink(path1, Misc.SymLinkFlag.Directory) ||
                     !string.Equals(Misc.GetRealPath(path1), path2, StringComparison.OrdinalIgnoreCase)))
                {
                    Directory.Delete(path1);
                    Misc.CreateSymbolicLink(path1, path2, Misc.SymLinkFlag.Directory);
                }
                else
                {
                    Misc.CreateSymbolicLink(path1, path2, Misc.SymLinkFlag.Directory);
                }

                //ExtractAiBarFiles
                var inputFile = Path.Combine(Program.UserConfig.GameFilesPath, "AI", "AI.bar");

                if (!File.Exists(inputFile))
                    throw new FileNotFoundException($"File '{inputFile}' not found!", inputFile);

                var outputPath = Path.Combine(Program.UserConfig.GameFilesPath, "AI");

                using (var fileStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var binReader = new BinaryReader(fileStream))
                    {
                        binReader.BaseStream.Seek(284, SeekOrigin.Begin);
                        var filesTableOffset = binReader.ReadUInt32();

                        binReader.BaseStream.Seek(filesTableOffset, SeekOrigin.Begin);

                        var rootNameLength = binReader.ReadUInt32();
                        binReader.BaseStream.Seek(rootNameLength * 2, SeekOrigin.Current);
                        var numberOfRootFiles = binReader.ReadUInt32();

                        for (uint i = 0; i < numberOfRootFiles; i++)
                        {
                            var offset = binReader.ReadInt32();
                            var fileSize = binReader.ReadInt32();

                            binReader.BaseStream.Seek(20, SeekOrigin.Current);

                            var lengthFileName = binReader.ReadUInt32();
                            var fileName = Encoding.Unicode.GetString(binReader.ReadBytes((int) lengthFileName * 2));

                            var baseDir = Path.GetDirectoryName(fileName) ?? string.Empty;
                            if (baseDir.ToLower().Contains("celeste"))
                                continue;

                            var position = binReader.BaseStream.Position;

                            //
                            binReader.BaseStream.Seek(offset, SeekOrigin.Begin);

                            var bytes = binReader.ReadBytes(fileSize);
                            
                            binReader.BaseStream.Seek(position, SeekOrigin.Begin);

                            var str = Encoding.Default.GetString(bytes);
                            if (!str.Contains("include \"aiMain.xs\";"))
                                continue;

                            //
                            var outFile = new FileInfo(Path.Combine(outputPath, baseDir));
                            if (outFile.Exists)
                            {
                                if (outFile.Length == fileSize && Crc32Utils.GetCrc32File(outFile.FullName) ==
                                    Crc32Utils.GetCrc32FromBytes(bytes))
                                    continue;

                                outFile.Delete();
                            }

                            //
                            var dir = new DirectoryInfo(Path.Combine(outputPath, baseDir));
                            if (!dir.Exists)
                                dir.Create();

                            if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                                dir.Attributes |= FileAttributes.Hidden;

                            //
                            File.WriteAllBytes(outFile.FullName, bytes);

                            if ((outFile.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                                outFile.Attributes |= FileAttributes.Hidden;
                        }
                    }
                }

                //
                Process.Start(
                    new ProcessStartInfo(spartanPath, $"LauncherLang={lang} LauncherLocale=1033")
                    {
                        WorkingDirectory = path
                    });

                WindowState = FormWindowState.Minimized;

                Close();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_Browse.Enabled = true;
        }

        private void Btn_Install_Editor_Click(object sender, EventArgs e)
        {
            Btn_Install_Editor.Enabled = false;
            btn_Browse.Enabled = false;
            try
            {
                using (var form = new GameScanProgressForm(_gameScannner))
                {
                    form.ShowDialog();

                    if (form.DialogResult != DialogResult.OK)
                        throw new Exception("Installation failed");

                    label2.Text = @"Installed";
                    label2.ForeColor = Color.Green;

                    btn_Browse.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
                    $"Error: Error during the installation of the 'Game Editor'. Error message: {exception.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                label2.Text = @"Non-Installed";
                label2.ForeColor = Color.Red;

                Btn_Install_Editor.Enabled = true;
            }
        }
    }
}