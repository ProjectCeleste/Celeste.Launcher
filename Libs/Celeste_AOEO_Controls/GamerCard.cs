#region Using directives

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class GamerCard : UserControl
    {
        public GamerCard()
        {
            InitializeComponent();
        }

        public Image Avatar
        {
            get => pictureBox1.Image;
            set => pictureBox1.Image = value;
        }

        public string UserName
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string Rank
        {
            get => label2.Text;
            set => label2.Text = value;
        }
    }
}