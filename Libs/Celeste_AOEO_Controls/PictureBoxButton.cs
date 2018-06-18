#region Using directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Celeste_AOEO_Controls
{
    public class PictureBoxButtonCustom : PictureBox
    {
        private Image _normalImage;

        private bool _showToolTip;

        private string _toolTipText;
        private Image _disabledImage;

        public PictureBoxButtonCustom()
        {
            InitializeComponent();

            if (!Enabled && DisabledImage != null)
                Image = DisabledImage;
            else if (Enabled && NormalImage != null)
                Image = NormalImage;
        }

        private ToolTip ToolTip { get; } = new ToolTip();

        public Image NormalImage
        {
            get => _normalImage;
            set
            {
                _normalImage = value;
                if (!Enabled && DisabledImage != null)
                    Image = DisabledImage;
                else if (Enabled && NormalImage != null)
                    Image = NormalImage;
            }
        }

        public Image DisabledImage
        {
            get => _disabledImage;
            set { _disabledImage = value;
                if (!Enabled && DisabledImage != null)
                    Image = DisabledImage;
                else if (Enabled && NormalImage != null)
                    Image = NormalImage;
            }
        }

        public Image HoverImage { get; set; }

        public string ToolTipText
        {
            get => _toolTipText;
            set
            {
                _toolTipText = value;

                if (_showToolTip && !string.IsNullOrEmpty(_toolTipText))
                {
                    ToolTip.RemoveAll();
                    ToolTip.SetToolTip(this, _toolTipText);
                }
                else
                {
                    ToolTip.RemoveAll();
                }
            }
        }

        public bool ShowToolTip
        {
            get => _showToolTip;
            set
            {
                _showToolTip = value;
                if (_showToolTip && !string.IsNullOrEmpty(ToolTipText))
                {
                    ToolTip.RemoveAll();
                    ToolTip.SetToolTip(this, ToolTipText);
                }
                else
                {
                    ToolTip.RemoveAll();
                }
            }
        }

        private void PictureBoxButtonCustom_MouseHover(object sender, EventArgs e)
        {
            if (Enabled && HoverImage != null)
                Image = HoverImage;
            else if (!Enabled && DisabledImage != null)
                Image = DisabledImage;
        }

        private void PictureBoxButtonCustom_MouseLeave(object sender, EventArgs e)
        {
            if (Enabled && NormalImage != null)
                Image = NormalImage;
            else if (!Enabled && DisabledImage != null)
                Image = DisabledImage;
        }

        private void This_EnabledChanged(object sender, EventArgs e)
        {
            if (!Enabled && DisabledImage != null)
                Image = DisabledImage;
            else if (Enabled && NormalImage != null)
                Image = NormalImage;
        }

        private void InitializeComponent()
        {
            ((ISupportInitialize) this).BeginInit();
            SuspendLayout();
            Cursor = Cursors.Hand;
            Size = new Size(64, 64);
            SizeMode = PictureBoxSizeMode.Zoom;
            MouseLeave += PictureBoxButtonCustom_MouseLeave;
            MouseHover += PictureBoxButtonCustom_MouseHover;
            EnabledChanged += This_EnabledChanged;
            ((ISupportInitialize) this).EndInit();
            ResumeLayout(false);
        }
    }
}