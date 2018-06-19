#region Using directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.Properties;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class CustomBtnSplit : UserControl
    {
        private string _text;

        public CustomBtnSplit()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        [DefaultValue(null)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string BtnText
        {
            get => _text;
            set
            {
                _text = value;
                lb_Btn.Text = _text;
            }
        }

        [DefaultValue(null)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ContextMenuStrip Menu { get; set; }
        
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

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Menu == null)
                return;
            var btnSender = (PictureBoxButtonCustom)sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            Menu.Show(btnSender, pt);
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (Size.Width > 180 || Size.Height > 45)
                BackgroundImage = Resources.BtnBigHover;
            else
                BackgroundImage = Resources.BtnSmallHover;
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (Size.Width > 180 || Size.Height > 45)
                BackgroundImage = Resources.BtnBigNormal;
            else
                BackgroundImage = Resources.BtnSmallNormal;
        }
    }
}