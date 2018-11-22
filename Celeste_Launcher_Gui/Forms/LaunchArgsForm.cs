#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class LaunchArgsForm : Form
    {
        public LaunchArgsForm()
        {
            InitializeComponent();
            SkinHelperFonts.SetFont(Controls);
            textBox1.Text = Program.UserConfig.AdditionalLaunchArgs;
        }

        private void pictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void customBtn1_Click(object sender, EventArgs e)
        {
            customBtn1.Enabled = false;
            Program.UserConfig.AdditionalLaunchArgs = textBox1.Text;
            MsgBox.ShowMessage("Applied new arguments");
            customBtn1.Enabled = true;
        }
    }
}