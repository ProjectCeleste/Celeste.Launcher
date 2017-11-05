#region Using directives

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_Public_Api;
using Celeste_Public_Api.GameFileInfo.Progress;
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
        }
        
        public void ProgressChanged(object sender, ExProgressGameFiles e)
        {
            pB_Progress.Value = e.ProgressPercentage;
            lbl_GlobalProgress.Text = $@"{e.CurrentIndex}/{e.TotalFile}";
        }

        private void MainContainer1_Load(object sender, EventArgs e)
        {

        }
    }
}