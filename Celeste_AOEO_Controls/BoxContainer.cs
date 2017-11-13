#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.Properties;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class BoxContainer : UserControl
    {
        public BoxContainer()
        {
            InitializeComponent();
        }

        public bool CloseButton
        {
            get => btn_Close.Visible;
            set => btn_Close.Visible = value;
        }

        private void Close(object sender, EventArgs e)
        {
            if (!CloseButton || ParentForm == null)
                return;

            ParentForm.DialogResult = DialogResult.Cancel;
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

        private void BoxContainer_Load(object sender, EventArgs e)
        {
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
}