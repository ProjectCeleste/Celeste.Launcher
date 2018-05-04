#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.GameScanner_Api;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class GameScan : Form
    {
        public GameScan()
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);

            if (Program.UserConfig != null && !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath))
                tb_GamePath.Text = Program.UserConfig.GameFilesPath;
            else
                tb_GamePath.Text = GameScannnerApi.GetGameFilesRootPath();

            if (Program.UserConfig != null && Program.UserConfig.IsLegacyXLive)
                rB_Legacy.Checked = true;
            else
                rB_Sparta.Checked = true;
        }

        private void BtnRunScan_Click(object sender, EventArgs e)
        {
            try
            {
                Program.UserConfig.GameFilesPath = tb_GamePath.Text;
                Program.UserConfig.IsLegacyXLive = rB_Legacy.Checked;
                Program.UserConfig.Save(Program.UserConfigFilePath);

                using (var form = new GameScanProgressForm(Program.UserConfig.GameFilesPath,
                    Program.UserConfig.IsSteamVersion, Program.UserConfig.IsLegacyXLive))
                {
                    var dr = form.ShowDialog();

                    if (dr == DialogResult.OK)
                        Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}",
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

        private void RB_Sparta_CheckedChanged(object sender, EventArgs e)
        {
            Program.UserConfig.IsLegacyXLive = !rB_Sparta.Checked;
        }

        private void RB_Legacy_CheckedChanged(object sender, EventArgs e)
        {
            Program.UserConfig.IsLegacyXLive = rB_Legacy.Checked;
        }
    }
}