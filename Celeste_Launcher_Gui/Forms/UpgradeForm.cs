#region Using directives

using System;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class UpgradeForm : Form
    {
        public UpgradeForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4694RNE7CLNE8");
        }
    }
}