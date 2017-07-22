#region Using directives

using System;
using System.Drawing;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Properties;

#endregion

namespace Celeste_Launcher_Gui.Controls
{
    public partial class BtnSmall : UserControl
    {
        private string _text;

        public BtnSmall()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        public string BtnText
        {
            get => _text;
            set
            {
                _text = value;
                lb_Btn.Text = _text;
            }
        }

        private void lb_Btn_MouseEnter(object sender, EventArgs e)
        {
            lb_Btn.ForeColor = Color.Yellow;
            BackgroundImage = Resources.BtnSmallHover;
        }

        private void lb_Btn_MouseLeave(object sender, EventArgs e)
        {
            lb_Btn.ForeColor = Color.White;
            BackgroundImage = Resources.BtnSmallNormal;
        }

        private void lb_Btn_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
    }
}