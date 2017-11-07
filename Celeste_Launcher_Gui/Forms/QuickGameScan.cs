#region Using directives

using System;
using System.Linq;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_Public_Api.GameScanner;
using Celeste_Public_Api.GameScanner.Models;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class QuickGameScan : Form
    {
        private string GameFilePath { get; }

        private GameScannnerApi GameScannner { get; }

        public QuickGameScan()
        {
            InitializeComponent();
            
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
                            @"Error: Your game files are corrupted or outdated. Click ""Yes"" to run a ""Game Scan"" to fix your game files, or ""No"" to close the launcher.")
                    )
                    {
                        var dr = form.ShowDialog();
                        if (dr != DialogResult.OK)
                            Environment.Exit(0);
                    }

                    using (var form = new GameScan())
                    {
                        form.ShowDialog();
                        DialogResult = DialogResult.Retry;
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
    }
}