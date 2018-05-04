#region Using directives

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

            if (!DownloadFileUtils.IsConnectedToInternet())
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

                Process.Start(
                    new ProcessStartInfo(spartanPath, $"LauncherLang={lang} LauncherLocale=1033")
                    {
                        WorkingDirectory = path
                    });

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