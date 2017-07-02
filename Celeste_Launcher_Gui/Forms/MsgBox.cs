#region Using directives

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MsgBox : Form
    {
        public MsgBox(string title, string message)
        {
            InitializeComponent();
            lb_Title.Text = title;
            lb_Message.Text = message;

            //Configure Skin
            SkinHelper.ConfigureSkin(this, lb_Title, lb_Close, new List<Label> {lb_OK});
        }

        private void lb_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}