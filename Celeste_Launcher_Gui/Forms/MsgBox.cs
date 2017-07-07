#region Using directives

using System;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MsgBox : Form
    {
        public MsgBox(string message)
        {
            InitializeComponent();
            label1.Text = message;

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        private void lb_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 31));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}