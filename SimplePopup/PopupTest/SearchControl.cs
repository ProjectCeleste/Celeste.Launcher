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
    public partial class SearchControl : UserControl
    {
        SearchProviders searchProviders;
        Popup popup;

        public SearchControl()
        {
            InitializeComponent();
            popup = new Popup(searchProviders = new SearchProviders());
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                popup.ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom;
                popup.HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop;
            }
            else
            {
                popup.ShowingAnimation = popup.HidingAnimation = PopupAnimations.None;
            }
            borderLabel.Image = searchProviders.ProviderImage;
            borderLabel.Text = searchProviders.ProviderName;
            popup.Closed += popup_Closed;
            searchProviders.ProviderChanged += fireSearch_Event;
        }

        public event EventHandler<StringEventArgs> Search;

        private void popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (!textBox.Visible)
            {
                borderLabel.Image = searchProviders.ProviderImage;
            }
            borderLabel.Text = searchProviders.ProviderName;
        }

        private void borderLabel_MouseDown(object sender, MouseEventArgs e)
        {
            textBox.Visible = true;
            textBox.Focus();
        }

        private void buttonDropDown_Click(object sender, EventArgs e)
        {
            popup.Width = Width;
            popup.Show(this);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                FireSearch();
            }
        }

        private void fireSearch_Event(object sender, EventArgs e)
        {
            FireSearch();
        }

        private void FireSearch()
        {
            if (textBox.Text.Length > 0 && Search != null)
            {
                Search(this, new StringEventArgs(searchProviders.SearchString + textBox.Text.Replace(' ', '+')));
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            borderLabel.Image = null;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (textBox.Text.Length == 0)
            {
                borderLabel.Image = searchProviders.ProviderImage;
                textBox.Visible = false;
            }
        }
    }
}
