#region Using directives

using System;
using System.Drawing;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Properties;

#endregion

namespace Celeste_AOEO_Controls
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

        private void Lb_Btn_MouseEnter(object sender, EventArgs e)
        {
            lb_Btn.ForeColor = Color.Yellow;
            BackgroundImage = Resources.Play_Button___Hover;
        }

        private void Lb_Btn_MouseLeave(object sender, EventArgs e)
        {
            lb_Btn.ForeColor = Color.White;
            BackgroundImage = Resources.Play_Button___Normal;
        }

        private void Lb_Btn_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
    }
}