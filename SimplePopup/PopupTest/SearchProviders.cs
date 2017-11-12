using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PopupControl;

namespace PopupTest
{
    public partial class SearchProviders : UserControl
    {
        public SearchProviders()
        {
            InitializeComponent();
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            SetColors(radioButton1);
            SetColors(radioButton2);
            SetColors(radioButton3);
            SetColors(radioButton4);
            (Parent as Popup).Close();
            if (ProviderChanged != null)
            {
                ProviderChanged(this, EventArgs.Empty);
            }
        }

        private void radioButton_MouseEnter(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            radio.ForeColor = Color.White;
            radio.BackColor = SystemColors.HotTrack;
        }

        private void radioButton_MouseLeave(object sender, EventArgs e)
        {
            SetColors(sender as RadioButton);
        }

        private static void SetColors(RadioButton radioButton)
        {
            if (radioButton.Checked)
            {
                radioButton.ForeColor = SystemColors.HighlightText;
                radioButton.BackColor = SystemColors.Highlight;
            }
            else
            {
                radioButton.ForeColor = SystemColors.WindowText;
                radioButton.BackColor = SystemColors.Window;
            }
        }

        public event EventHandler ProviderChanged;

        public string SearchString
        {
            get
            {
                if (radioButton1.Checked)
                {
                    return "http://www.google.com/search?q=";
                }
                if (radioButton2.Checked)
                {
                    return "http://www.codeproject.com/info/search.asp?searchkw=";
                }
                if (radioButton3.Checked)
                {
                    return "http://en.wikipedia.org/w/index.php?title=Special:Search&search=";
                }
                return "http://search.msdn.microsoft.com/search/default.aspx?siteId=0&tab=0&query=";
            }
        }

        public string ProviderName
        {
            get
            {
                if (radioButton1.Checked)
                {
                    return "Google";
                }
                if (radioButton2.Checked)
                {
                    return "Code Project";
                }
                if (radioButton3.Checked)
                {
                    return "Wikipedia";
                }
                return "MSDN";
            }
        }

        public Image ProviderImage
        {
            get
            {
                if (radioButton1.Checked)
                {
                    return radioButton1.Image;
                }
                if (radioButton2.Checked)
                {
                    return radioButton2.Image;
                }
                if (radioButton3.Checked)
                {
                    return radioButton3.Image;
                }
                return radioButton4.Image;
            }
        }
    }
}
