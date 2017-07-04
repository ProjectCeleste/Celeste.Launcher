#region Using directives

using System;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Controls
{
    // [Designer(typeof(MyCustomControlDesigner1))]
    public partial class MainContainer : UserControl
    {
        private string _title;

        public MainContainer()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                lb_Title.Text = _title;
            }
        }

        // ReSharper disable once ConvertToAutoProperty
        public Panel ContainerPanel => panel9;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ParentForm?.Close();
        }

        private void lb_Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SkinHelper.ReleaseCapture();
                var findForm = ((Label) sender).FindForm();
                if (findForm != null)
                    SkinHelper.SendMessage(findForm.Handle, SkinHelper.WM_NCLBUTTONDOWN, SkinHelper.HT_CAPTION, 0);
            }
        }
    }

    //public class MyCustomControlDesigner1 : ParentControlDesigner
    //{
    //    public override void Initialize(IComponent component)
    //    {
    //        base.Initialize(component);

    //        var control = Control as MainContainer;
    //        if (control != null)
    //            EnableDesignMode(control.ContainerPanel, "WorkingArea");
    //    }
    //}
}