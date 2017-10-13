using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Celeste_AOEO_Controls
{
    public partial class PictureBoxButtonCustom : PictureBox
    {
        private Image znormalImage, zhoverImage, zpressedImage;
        public PictureBoxButtonCustom()
        {
            InitializeComponent();
            this.Cursor = Cursors.Hand;
        }
         
       
        // sets the normal image
        public Image zNormalImage
        {
            get { return znormalImage; }
            set { znormalImage = value; }
        }

       
         

        private void PictureBoxButtonCustom_MouseHover(object sender, EventArgs e)
        {
            // on mouse hover
            if (zhoverImage != null)
            {
                this.Image = zhoverImage;
            }
        }

        private void PictureBoxButtonCustom_MouseLeave(object sender, EventArgs e)
        {
            // when mouse leaves the button arena
            if (znormalImage != null)
            {
                this.Image = znormalImage;
            }
        }



        // sets the hover image
        public Image zHoverImage
        {
            get { return zhoverImage; }
            set { zhoverImage = value; }
        }

        // pressed image, sets the image when button is pressed
        public Image zPressedImage
        {
            get { return zpressedImage; }
            set { zpressedImage = value; }
        }


    }
}
