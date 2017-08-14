#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Properties;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class BoxContainer : UserControl
    {
        public BoxContainer()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        public bool CloseButton
        {
            get => btn_Close.Visible;
            set => btn_Close.Visible = value;
        }

        // ReSharper disable once ConvertToAutoProperty
        //public Panel ContainerPanel => panel9;

        private void Close(object sender, EventArgs e)
        {
            if (CloseButton)
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
    }
}