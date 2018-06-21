#region Using directives

using System;
using System.Drawing;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.Properties;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class CustomBtn : UserControl
    {
        private string _text;

        public CustomBtn()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelperFonts.SetFont(Controls);
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

            if (Size.Width > 180 || Size.Height > 45)
                BackgroundImage = Resources.BtnBigHover;
            else
                BackgroundImage = Resources.BtnSmallHover;
        }

        private void Lb_Btn_MouseLeave(object sender, EventArgs e)
        {
            lb_Btn.ForeColor = Color.White;

            if (Size.Width > 180 || Size.Height > 45)
                BackgroundImage = Resources.BtnBigNormal;
            else
                BackgroundImage = Resources.BtnSmallNormal;
        }

        private void Lb_Btn_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void CustomBtn_Load(object sender, EventArgs e)
        {
            if (Size.Width > 180 || Size.Height > 45)
                BackgroundImage = Resources.BtnBigNormal;
            else
                BackgroundImage = Resources.BtnSmallNormal;
        }
    }
}