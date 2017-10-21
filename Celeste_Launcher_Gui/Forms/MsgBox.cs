#region Using directives

using System;
using System.Windows.Forms;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MsgBox : Form
    {
        public MsgBox(string message)
        {
            InitializeComponent();
            label1.Text = message;
        }

        private void Lb_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}