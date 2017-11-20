#region Using directives

using System;
using System.Linq;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.GameScanner_Api.Models;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class QuickGameScan : Form
    {
        public QuickGameScan()
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);

            GameFilePath = Program.UserConfig != null && !string.IsNullOrEmpty(Program.UserConfig.GameFilesPath)
                ? Program.UserConfig.GameFilesPath
                : GameScannnerApi.GetGameFilesRootPath();

            GameScannner = Program.UserConfig != null
                ? new GameScannnerApi(Program.UserConfig.BetaUpdate, GameFilePath)
                : new GameScannnerApi(false, GameFilePath);

            pB_Progress.Value = 0;
            lbl_GlobalProgress.Text = $@"0/{GameScannner.FilesInfo.Count()}";

            Shown += MainContainer1_Shown;
        }

        private string GameFilePath { get; }

        private GameScannnerApi GameScannner { get; }

        private async void MainContainer1_Shown(object sender, EventArgs e)
        {
            try
            {
                pB_Progress.Value = 0;
                lbl_GlobalProgress.Text = $@"0/{GameScannner.FilesInfo.Count()}";

                var progress = new Progress<ScanAndRepairProgress>();
                progress.ProgressChanged += (o, ea) =>
                {
                    pB_Progress.Value = ea.ProgressPercentage;
                    lbl_GlobalProgress.Text = $@"{ea.CurrentIndex}/{ea.TotalFile}";
                };

                if (await GameScannner.QuickScan(progress))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    using (var form =
                        new MsgBoxYesNo(
                            @"Error: Your game files are corrupted or outdated. Click ""Yes"" to run a ""Game Scan"" to fix your game files, or ""No"" to ignore the error (not recommended).")
                    )
                    {
                        var dr = form.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            using (var form2 = new GameScan())
                            {
                                form2.ShowDialog();
                                DialogResult = DialogResult.Retry;
                            }
                        }
                        else
                        {
                            DialogResult = dr;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.Abort;

                MsgBox.ShowMessage($@"Error: {ex.Message}",
                    @"Project Celeste -- Game Scan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }

        private void QuickGameScan_Load(object sender, EventArgs e)
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