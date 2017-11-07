#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_Public_Api;
using Celeste_Public_Api.GameFiles;
using Celeste_Public_Api.GameFiles.Progress;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class QuickGameScan : Form
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        public QuickGameScan()
        {
            InitializeComponent();
            Shown += MainContainer1_Shown;
        }
        
        public void ProgressChanged(object sender, ExProgressGameFilesQ e)
        {
            pB_Progress.Value = e.ProgressPercentage;
            lbl_GlobalProgress.Text = $@"{e.CurrentIndex}/{e.TotalFile}";
        }

        private async void MainContainer1_Shown(object sender, EventArgs e)
        {
            try
           {
                var gameFilePath = Program.UserConfig != null && !string.IsNullOrEmpty(Program.UserConfig.GameFilesPath)
                    ? Program.UserConfig.GameFilesPath
                    : GameFileApi.FindGameFileDirectory();
                
                if (await GameFileApi.GameFiles.QuickScan(gameFilePath, ProgressChanged))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    using (var form = new MsgBoxYesNo(@"Error: Your game files are corrupted or outdated. Click ""Yes"" to run a ""Game Scan"" to fix your game files, or ""No"" to close the launcher."))
                    {
                        var dr = form.ShowDialog();
                        if (dr != DialogResult.OK)
                        {
                            Environment.Exit(0);
                        }
                    }
                    
                    using (var form = new GameScan())
                    {
                        var dr = form.ShowDialog();
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