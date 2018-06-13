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
    public partial class OfflineModeForm : Form
    {
        private readonly GameScannnerApi _gameScannner;
        public bool internetAccess;
        public OfflineModeForm()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    internetAccess = true;
                }
            }
            catch
            {
                internetAccess = false;
            }

            if (internetAccess == true)
            {
                var path = !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath)
                ? Program.UserConfig.GameFilesPath
                : GameScannnerApi.GetGameFilesRootPath();

                _gameScannner = GameScannnerApi.InstallGameEditor(Program.UserConfig.IsSteamVersion,
                    Program.UserConfig.IsLegacyXLive,
                    path);
            }

            //if (Directory.Exists(""))
            //{

            //}

            InitializeComponent();
        }

        private async void EditorForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.UserConfig.GameFilesPath) || !File.Exists(Program.UserConfig.GameFilesPath + "\\Spartan.exe"))
            {
                MsgBox.ShowMessage("Please run a game scan before using the editor or offline mode.");
                safeFormClose();
                MainForm mf = new MainForm();
                mf.ToolStripMenuItem1_Click(sender, e);
                goto end;
            }
            if (!Directory.Exists(Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Spartan\\Scenario"))
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Spartan\\Scenario");
            }

            FolderListener();
            refreshList();
            folderListener.EnableRaisingEvents = true;

            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            if (internetAccess == true)
            {
                if (await _gameScannner.QuickScan())
                {
                    Btn_Install_Editor.Enabled = false;
                    btn_Editor.Enabled = true;

                    label2.Text = @"✓";
                    label2.ForeColor = Color.Green;
                }
                else
                {
                    label2.Text = @"✕";
                    label2.ForeColor = Color.Red;

                    Btn_Install_Editor.Enabled = true;
                    btn_Editor.Enabled = false;
                }
            }
            else
            {
                Btn_Install_Editor.Enabled = true;
                btn_Editor.Enabled = true;

                label2.Text = @"?";
                label2.ForeColor = Color.Black;
            }
            end:;
        }

        public void refreshList()
        {
            listBox1.Items.Clear();
            string filepath1 = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Spartan\\Scenario";
            DirectoryInfo d1 = new DirectoryInfo(filepath1);

            if (Directory.Exists(Program.UserConfig.GameFilesPath) && File.Exists(Program.UserConfig.GameFilesPath + "\\Spartan.exe"))
            {
                try
                {
                    foreach (var file in d1.GetFiles("*.age4scn"))
                    {
                        listBox1.Items.Add(Path.GetFileNameWithoutExtension(file.FullName.ToString()));
                    }
                }
                catch (Exception err)
                {
                    MsgBox.ShowMessage(
                    $"Warning: Error during quick scan. Error message: {err.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MsgBox.ShowMessage("Please run a Game Scan first");
                safeFormClose();
            }
        }

        private void safeFormClose()
        {
            folderListener.EnableRaisingEvents = false;
            Close();
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            safeFormClose();
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            var pname = Process.GetProcessesByName("editor");
            if (pname.Length > 0)
            {
                MsgBox.ShowMessage("Editor already running!");
                return;
            }

            btn_Editor.Enabled = false;
            try
            {
                btn_Editor.Enabled = false;

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
                    Directory.Delete(path1, true);
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
                            var fileName = Encoding.Unicode.GetString(binReader.ReadBytes((int)lengthFileName * 2));

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
                            var outFile = new FileInfo(Path.Combine(outputPath, fileName));
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

                Process.Start(
                    new ProcessStartInfo(spartanPath, $"LauncherLang={lang} LauncherLocale=1033")
                    {
                        WorkingDirectory = path
                    });
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            btn_Editor.Enabled = true;
        }


        private void Btn_Install_Editor_Click(object sender, EventArgs e)
        {
            Btn_Install_Editor.Enabled = false;
            btn_Editor.Enabled = false;
            if (internetAccess == true)
            {
                try
                {
                    using (var form = new GameScanProgressForm(_gameScannner))
                    {
                        form.ShowDialog();

                        if (form.DialogResult != DialogResult.OK)
                            throw new Exception("Installation failed");

                        label2.Text = @"OK";
                        label2.ForeColor = Color.Green;

                        btn_Editor.Enabled = true;
                    }
                }
                catch (Exception exception)
                {
                    MsgBox.ShowMessage(
                        $"Error: Error during the installation of the 'Game Editor'. Error message: {exception.Message}",
                        @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    label2.Text = @"Missing";
                    label2.ForeColor = Color.Red;

                    Btn_Install_Editor.Enabled = true;
                }
            } else
            {
                MsgBox.ShowMessage("Internet Connection is not available. Functions such as editor installation are limited.");
            }
            Btn_Install_Editor.Enabled = true;
            btn_Editor.Enabled = true;
        }

        public void btnOfflineLaunch(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.offlineLaunch();
        }

        

        private void btnShow1_Click(object sender, EventArgs e)
        {
            String ScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Spartan\\Scenario";
            Process.Start(ScenPath);
        }

        private void importScenario(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Scenario files (*.age4scn)|*.age4scn",
                CheckFileExists = true,
                Title = @"Choose Scenario File",
                Multiselect = false
            };
            dlg.ShowDialog();
            if (dlg.FileName == string.Empty)
            {
                Console.WriteLine("No file was chosen, closing OpenFileDialog");
            }
            else
            {
                try
                {
                    String ScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Spartan\\Scenario";
                    String selectedFullPath = dlg.FileName;
                    String selectedFileOnly = Path.GetFileName(selectedFullPath);
                    String selectedDestinationPath = ScenPath + "\\" + selectedFileOnly;
                    File.Move(selectedFullPath, selectedDestinationPath);
                    refreshList();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }
            refreshList();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/threads/read-me.3428/");
        }

        private void btnDlMore_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/forums/custom-scenario-sharing.79/");
        }

        public void FolderListener()
        {
            folderListener.Path = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Spartan\\Scenario";
            folderListener.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            folderListener.Filter = "*.*";
            folderListener.Changed += new FileSystemEventHandler(FolderListenerEvent);
            folderListener.Created += new FileSystemEventHandler(FolderListenerEvent);
            folderListener.Deleted += new FileSystemEventHandler(FolderListenerEvent);
            folderListener.Renamed += new RenamedEventHandler(FolderListenerEvent);
            folderListener.EnableRaisingEvents = true;
            folderListener.IncludeSubdirectories = true;
        }

        private void FolderListenerEvent(object source, FileSystemEventArgs e)
        {
            try
            {
                refreshList();
            }
            catch (ObjectDisposedException)
            {
                // Caught disposed object
            }
        }

        private void editorExited(object sender, EventArgs e)
        {
            MsgBox.ShowMessage("Exited!");
        }

        private void offlineModeExited(object sender, EventArgs e)
        {

        }
    }
}