#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_AOEO_Controls.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class LanguageChooser : Form
    {
        public LanguageChooser()
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);
        }

        public GameLanguage SelectedLang { get; private set; }

        private void QuickGameScan_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(8, 8, 8, 8));
            }
            catch (Exception)
            {
                //
            }
        }

        private void DeDE_Click(object sender, EventArgs e)
        {
            SelectedLang = GameLanguage.deDE;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void EnUS_Click(object sender, EventArgs e)
        {
            SelectedLang = GameLanguage.enUS;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PictureBoxButtonCustom2_Click(object sender, EventArgs e)
        {
            SelectedLang = GameLanguage.esES;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PictureBoxButtonCustom5_Click(object sender, EventArgs e)
        {
            SelectedLang = GameLanguage.frFR;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PictureBoxButtonCustom4_Click(object sender, EventArgs e)
        {
            SelectedLang = GameLanguage.itIT;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PictureBoxButtonCustom6_Click(object sender, EventArgs e)
        {
            SelectedLang = GameLanguage.zhCHT;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}