#region Using directives

using System;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class NewsSideShow : UserControl
    {
        public NewsSideShow()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com");
        }
    }
}