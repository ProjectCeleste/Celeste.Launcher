#region Using directives

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.Properties;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class MainContainer : UserControl
    {
        public MainContainer()
        {
            InitializeComponent();

            ////Set Skin
            //TopLeftFixed.BackgroundImage = CustomSkinMainContainer.TopLeftFixed;
            //TopRigthFixed.BackgroundImage = CustomSkinMainContainer.TopRigthFixed;
            //TopMiddleFluid.BackgroundImage = CustomSkinMainContainer.TopMiddleFluid;
        }


        public bool CloseButton
        {
            get => btn_Close.Visible;
            set => btn_Close.Visible = value;
        }

        public bool MinimizeBox { get; set; }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            ParentForm?.Close();
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            SkinHelper.ReleaseCapture();
            var findForm = ((Control) sender).FindForm();
            if (findForm != null)
                SkinHelper.SendMessage(findForm.Handle, SkinHelper.WM_NCLBUTTONDOWN, SkinHelper.HT_CAPTION, 0);
        }

        private void Btn_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_Close.Image = Resources.XButton_Hover;
        }

        private void Btn_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_Close.Image = Resources.XButton_Normal;
        }

        private void PictureBox1_Click_1(object sender, EventArgs e)
        {
            if (ParentForm != null)
                ParentForm.WindowState = FormWindowState.Minimized;
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.MinimizeButtonHover;
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.MinimizeButtonNormal;
        }

        private void MainContainer_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = MinimizeBox;

            var parentForm = ParentForm;
            if (parentForm != null)
            {
                SkinHelper.SetFont(parentForm.Controls);
                try
                {
                    if (DwmApi.DwmIsCompositionEnabled())
                        DwmApi.DwmExtendFrameIntoClientArea(parentForm.Handle, new DwmApi.MARGINS(26, 75, 26, 26));
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                //Configure Fonts
                SkinHelper.SetFont(Controls);
            }
        }
    }

    public static class CustomSkinMainContainer
    {
        private static Image _topLeftFixed;

        private static Image _topRigthFixed;

        private static Image _topMiddleFluid;

        public static Image TopLeftFixed
        {
            get
            {
                if (_topLeftFixed != null)
                    return _topLeftFixed;

                _topLeftFixed = File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Skin\\TopLeftFixed.png")
                    ? Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Skin\\TopLeftFixed.png")
                    : Resources.TopLeftFixed;
                return _topLeftFixed;
            }
            set => _topLeftFixed = value;
        }

        public static Image TopRigthFixed
        {
            get
            {
                if (_topRigthFixed != null)
                    return _topRigthFixed;

                _topRigthFixed = File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Skin\\TopRigthFixed.png")
                    ? Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Skin\\TopRigthFixed.png")
                    : Resources.TopRigthFixed;
                return _topRigthFixed;
            }
            set => _topRigthFixed = value;
        }

        public static Image TopMiddleFluid
        {
            get
            {
                if (_topMiddleFluid != null)
                    return _topMiddleFluid;

                _topMiddleFluid = File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Skin\\TopMiddleFluid.png")
                    ? Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Skin\\TopMiddleFluid.png")
                    : Resources.TopMiddleFluid;
                return _topMiddleFluid;
            }
            set => _topMiddleFluid = value;
        }
    }
}