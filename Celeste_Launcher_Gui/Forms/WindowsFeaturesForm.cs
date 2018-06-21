#region Using directives

using System;
using System.Drawing;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Microsoft.Dism;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class WindowsFeaturesForm : Form
    {
        public WindowsFeaturesForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void WindowsFeatures_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception)
            {
                //
            }

            //Windows features
            try
            {
                foreach (var feature in await Dism.GetWindowsFeatureInfo(new[] {"DirectPlay", "NetFx3"}))
                    if (string.Equals(feature.Key, "DirectPlay", StringComparison.CurrentCultureIgnoreCase))
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (feature.Value.FeatureState)
                        {
                            case DismPackageFeatureState.Staged:
                                l_DirectPlayState.Text = @"Staged";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                btn_FixDirectPlay.Enabled = true;
                                break;
                            case DismPackageFeatureState.PartiallyInstalled:
                                l_DirectPlayState.Text = @"Partially installed";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                btn_FixDirectPlay.Enabled = true;
                                break;
                            case DismPackageFeatureState.Installed:
                                l_DirectPlayState.Text = @"Installed";
                                l_DirectPlayState.ForeColor = Color.DarkGreen;
                                btn_FixDirectPlay.Enabled = false;
                                break;
                            case DismPackageFeatureState.InstallPending:
                                l_DirectPlayState.Text = @"Install pending";
                                l_DirectPlayState.ForeColor = Color.DarkGreen;
                                btn_FixDirectPlay.Enabled = false;
                                break;
                            default:
                                l_DirectPlayState.Text = $@"Not supported ({feature.Value.FeatureState})";
                                l_DirectPlayState.ForeColor = Color.Red;
                                btn_FixDirectPlay.Enabled = false;
                                break;
                        }
                    else if (string.Equals(feature.Key, "NetFx3", StringComparison.CurrentCultureIgnoreCase))
                        // ReSharper disable once SwitchStatementMissingSomeCases
                        switch (feature.Value.FeatureState)
                        {
                            case DismPackageFeatureState.Staged:
                                l_NetFx3State.Text = @"Staged";
                                l_NetFx3State.ForeColor = Color.Chocolate;
                                btn_FixNetFx.Enabled = true;
                                break;
                            case DismPackageFeatureState.PartiallyInstalled:
                                l_NetFx3State.Text = @"Partially installed";
                                l_NetFx3State.ForeColor = Color.Chocolate;
                                btn_FixNetFx.Enabled = true;
                                break;
                            case DismPackageFeatureState.Installed:
                                l_NetFx3State.Text = @"Installed";
                                l_NetFx3State.ForeColor = Color.DarkGreen;
                                btn_FixNetFx.Enabled = false;
                                break;
                            case DismPackageFeatureState.InstallPending:
                                l_NetFx3State.Text = @"Install pending";
                                l_NetFx3State.ForeColor = Color.DarkGreen;
                                btn_FixNetFx.Enabled = false;
                                break;
                            default:
                                l_NetFx3State.Text = $@"Not supported ({feature.Value.FeatureState})";
                                l_NetFx3State.ForeColor = Color.Red;
                                btn_FixNetFx.Enabled = false;
                                break;
                        }
            }
            catch (Exception)
            {
                l_DirectPlayState.Text = @"Not supported (unknow error)";
                l_DirectPlayState.ForeColor = Color.Red;
            }
        }

        private void DismProgress(DismProgress e)
        {
            progressBar1.Value = Convert.ToInt32(Math.Floor((double) e.Current / e.Total * 100));
        }

        private async void Btn_FixDirectPlay_Click(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                var feature = await Dism.EnableWindowsFeatures("DirectPlay", DismProgress);
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (feature.FeatureState)
                {
                    case DismPackageFeatureState.Staged:
                        l_DirectPlayState.Text = @"Staged";
                        l_DirectPlayState.ForeColor = Color.Chocolate;
                        btn_FixDirectPlay.Enabled = true;
                        break;
                    case DismPackageFeatureState.PartiallyInstalled:
                        l_DirectPlayState.Text = @"Partially installed";
                        l_DirectPlayState.ForeColor = Color.Chocolate;
                        btn_FixDirectPlay.Enabled = true;
                        break;
                    case DismPackageFeatureState.Installed:
                        l_DirectPlayState.Text = @"Installed";
                        l_DirectPlayState.ForeColor = Color.DarkGreen;
                        btn_FixDirectPlay.Enabled = false;
                        break;
                    case DismPackageFeatureState.InstallPending:
                        l_DirectPlayState.Text = @"Install pending";
                        l_DirectPlayState.ForeColor = Color.DarkGreen;
                        btn_FixDirectPlay.Enabled = false;
                        break;
                    default:
                        l_DirectPlayState.Text = $@"Not supported ({feature.FeatureState})";
                        l_DirectPlayState.ForeColor = Color.Red;
                        btn_FixDirectPlay.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Enabled = true;
        }

        private async void Btn_FixNetFx_Click(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                var feature = await Dism.EnableWindowsFeatures("NetFx3", DismProgress);
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (feature.FeatureState)
                {
                    case DismPackageFeatureState.Staged:
                        l_NetFx3State.Text = @"Staged";
                        l_NetFx3State.ForeColor = Color.Chocolate;
                        btn_FixNetFx.Enabled = true;
                        break;
                    case DismPackageFeatureState.PartiallyInstalled:
                        l_NetFx3State.Text = @"Partially installed";
                        l_NetFx3State.ForeColor = Color.Chocolate;
                        btn_FixNetFx.Enabled = true;
                        break;
                    case DismPackageFeatureState.Installed:
                        l_NetFx3State.Text = @"Installed";
                        l_NetFx3State.ForeColor = Color.DarkGreen;
                        btn_FixNetFx.Enabled = false;
                        break;
                    case DismPackageFeatureState.InstallPending:
                        l_NetFx3State.Text = @"Install pending";
                        l_NetFx3State.ForeColor = Color.DarkGreen;
                        btn_FixNetFx.Enabled = false;
                        break;
                    default:
                        l_NetFx3State.Text = $@"Not supported ({feature.FeatureState})";
                        l_NetFx3State.ForeColor = Color.Red;
                        btn_FixNetFx.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Enabled = true;
        }
    }
}