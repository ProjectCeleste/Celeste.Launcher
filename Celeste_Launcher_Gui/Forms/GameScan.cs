#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using ProjectCeleste.GameFiles.GameScanner;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class GameScan : Form
    {
        public GameScan()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);

            if (Program.UserConfig != null && !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath))
                tb_GamePath.Text = Program.UserConfig.GameFilesPath;
            else
                tb_GamePath.Text = GameScannnerManager.GetGameFilesRootPath();
        }

        private void BtnRunScan_Click(object sender, EventArgs e)
        {
            try
            {
                Program.UserConfig.GameFilesPath = tb_GamePath.Text;
                Program.UserConfig.Save(Program.UserConfigFilePath);

                using (var form = new GameScanProgressForm(Program.UserConfig.GameFilesPath,
                    Program.UserConfig.IsSteamVersion, checkBox1.Checked))
                {
                    var dr = form.ShowDialog();

                    if (dr == DialogResult.OK)
                        Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Exception: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog {ShowNewFolderButton = true})
            {
                fbd.ShowDialog();
                tb_GamePath.Text = fbd.SelectedPath;
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GameScan_Load(object sender, EventArgs e)
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
        }
    }
}