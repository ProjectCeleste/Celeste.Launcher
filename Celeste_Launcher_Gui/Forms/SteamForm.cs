#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class SteamForm : Form
    {
        public SteamForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);
        }

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

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                var exePath = Assembly.GetEntryAssembly().Location;
                if (exePath.EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Celeste Fan Project Launcher is already compatible with \"Steam\".");

                var exeFolder = Path.GetDirectoryName(exePath);
                if (!string.Equals(Program.UserConfig.GameFilesPath, exeFolder, StringComparison.OrdinalIgnoreCase))
                    throw new Exception(
                        "Celeste Fan Project Launcher need to be installed in the same folder has the game.");

                Steam.ConvertToSteam(Program.UserConfig.GameFilesPath);

                MsgBox.ShowMessage(
                    @"""Celeste Fan Project Launcher"" is now compatible with ""Steam"", it will now re-start.",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(Assembly.GetEntryAssembly().Location
                    .Replace("Celeste_Launcher_Gui.exe", "AOEOnline.exe"));

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}