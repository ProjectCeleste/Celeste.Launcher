#region Using directives

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class UpgradeForm : Form
    {
        public UpgradeForm()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4694RNE7CLNE8");
        }

        private void UpgradeForm_Load(object sender, EventArgs e)
        {
            if (DwmApi.DwmIsCompositionEnabled())
                DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 31));
        }
    }
}