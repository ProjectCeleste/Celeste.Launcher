#region Using directives

using System;
using System.Drawing;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
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

            SkinHelper.SetFont(Controls);
        }
        
        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void GameScan_Load(object sender, EventArgs e)
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
                foreach (var feature in await Dism.GetWindowsFeatureInfo(new[] { "DirectPlay", "NetFx3" }))
                {
                    if (string.Equals(feature.Key, "DirectPlay", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (feature.Value.FeatureState)
                        {
                            case DismPackageFeatureState.NotPresent:
                                l_DirectPlayState.Text = @"Not Present";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.UninstallPending:
                                l_DirectPlayState.Text = @"Uninstall Pending";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Staged:
                                l_DirectPlayState.Text = @"Staged";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Resolved:
                                l_DirectPlayState.Text = @"Resolved";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Installed:
                                l_DirectPlayState.Text = @"Installed";
                                l_DirectPlayState.ForeColor = Color.DarkGreen;
                                break;
                            case DismPackageFeatureState.InstallPending:
                                l_DirectPlayState.Text = @"Install Pending";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Superseded:
                                l_DirectPlayState.Text = @"Superseded";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.PartiallyInstalled:
                                l_DirectPlayState.Text = @"PartiallyInstalled";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else if (string.Equals(feature.Key, "NetFx3", StringComparison.CurrentCultureIgnoreCase))
                        switch (feature.Value.FeatureState)
                        {
                            case DismPackageFeatureState.NotPresent:
                                l_DirectPlayState.Text = @"Not Present";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.UninstallPending:
                                l_DirectPlayState.Text = @"Uninstall Pending";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Staged:
                                l_DirectPlayState.Text = @"Staged";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Resolved:
                                l_DirectPlayState.Text = @"Resolved";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Installed:
                                l_DirectPlayState.Text = @"Installed";
                                l_DirectPlayState.ForeColor = Color.DarkGreen;
                                break;
                            case DismPackageFeatureState.InstallPending:
                                l_DirectPlayState.Text = @"Install Pending";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.Superseded:
                                l_DirectPlayState.Text = @"Superseded";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            case DismPackageFeatureState.PartiallyInstalled:
                                l_DirectPlayState.Text = @"PartiallyInstalled";
                                l_DirectPlayState.ForeColor = Color.Chocolate;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                }
            }
            catch (Exception)
            {
                l_DirectPlayState.Text = @"Not Supported";
                l_DirectPlayState.ForeColor = Color.Chocolate;
            }
        }
    }
}