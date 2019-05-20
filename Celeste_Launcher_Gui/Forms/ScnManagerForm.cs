#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class ScnManagerForm : Form
    {
        private readonly FileSystemWatcher _folderListener;

        private readonly string _scnPath =
            Path.Combine(Environment.GetEnvironmentVariable("userprofile") ?? string.Empty, "Documents", "Spartan",
                "Scenario");

        private readonly object _syncLock = new object();

        public ScnManagerForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);

            if (!Directory.Exists(_scnPath))
                Directory.CreateDirectory(_scnPath);

            RefreshList();

            _folderListener = new FileSystemWatcher(_scnPath, "*.age4scn")
            {
                NotifyFilter = //NotifyFilters.Attributes | 
                    NotifyFilters.CreationTime |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.FileName |
                    //NotifyFilters.LastAccess | 
                    NotifyFilters.LastWrite |
                    //NotifyFilters.Security |
                    NotifyFilters.Size
            };
            _folderListener.Changed += FolderListenerEvent;
            _folderListener.Created += FolderListenerEvent;
            _folderListener.Deleted += FolderListenerEvent;
            _folderListener.Renamed += FolderListenerEvent;
            _folderListener.IncludeSubdirectories = true;
            _folderListener.EnableRaisingEvents = true;
        }

        private void EditorForm_Load(object sender, EventArgs e)
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
        }

        public void RefreshList()
        {
            if (!Monitor.TryEnter(_syncLock))
                return;

            try
            {
                listView1.Items.Clear();
                foreach (var filePath in Directory.GetFiles(_scnPath, "*.age4scn", SearchOption.AllDirectories))
                    try
                    {
                        var txt = filePath.Replace(_scnPath, string.Empty).Replace(".age4scn", string.Empty);
                        if (txt.StartsWith(@"/") || txt.StartsWith(@"\"))
                            txt = txt.Substring(1);

                        listView1.Items.Add(new ListViewItem
                        {
                            Text = txt,
                            Tag = filePath
                        });
                    }
                    catch (Exception)
                    {
                        //
                    }
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                Monitor.Exit(_syncLock);
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FolderListenerEvent(object source, FileSystemEventArgs e)
        {
            listView1.Invoke((MethodInvoker) RefreshList);
        }

        private void OfflineModeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _folderListener.Dispose();
        }

        private void PictureBoxButtonCustom5_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/resources/categories/custom-scenario.2/");
        }

        private void PictureBoxButtonCustom2_Click(object sender, EventArgs e)
        {
            _folderListener.EnableRaisingEvents = false;

            try
            {
                using (var dlg = new OpenFileDialog
                {
                    Filter = @"Scenario files (*.age4scn)|*.age4scn",
                    CheckFileExists = true,
                    Title = @"Choose Scenario File",
                    Multiselect = true
                })
                {
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    foreach (var filename in dlg.FileNames)
                    {
                        var selectedDestinationPath =
                            Path.Combine(_scnPath, Path.GetFileName(filename) ?? string.Empty);
                        try
                        {
                            if (File.Exists(selectedDestinationPath))
                                using (var form =
                                    new MsgBoxYesNo(
                                        $@"The file already exist ""{
                                                selectedDestinationPath
                                            }"". Click ""Yes"" to overwrite it, or ""No"" to ignore it.")
                                )
                                {
                                    var dr = form.ShowDialog();
                                    if (dr != DialogResult.OK)
                                        return;
                                }

                            File.Copy(filename, selectedDestinationPath, true);
                        }
                        catch (Exception)
                        {
                            //
                        }
                    }

                    RefreshList();
                }
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                _folderListener.EnableRaisingEvents = true;
            }
        }

        private void PictureBoxButtonCustom3_Click(object sender, EventArgs e)
        {
            _folderListener.EnableRaisingEvents = false;
            try
            {
                if (listView1.SelectedItems.Count < 1)
                    return;
                foreach (ListViewItem lvi in listView1.SelectedItems)
                    try
                    {
                        if (File.Exists((string) lvi.Tag))
                            using (var form =
                                new MsgBoxYesNo(
                                    $@"Are you sure to want to remove ""{
                                            lvi.Text
                                        }"" ? Click ""Yes"" to remove it, or ""No"" to cancel.")
                            )
                            {
                                var dr = form.ShowDialog();
                                if (dr != DialogResult.OK)
                                    return;
                            }

                        File.Delete((string) lvi.Tag);
                    }
                    catch (Exception)
                    {
                        //
                    }
                RefreshList();
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                _folderListener.EnableRaisingEvents = true;
            }
        }

        private void PictureBoxButtonCustom4_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_scnPath))
                Process.Start(_scnPath);
        }

        private void PictureBoxButtonCustom6_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/wiki/offline-mode/");
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxButtonCustom3.Enabled = listView1.SelectedItems.Count > 0;
        }

        private void CustomBtn1_Click(object sender, EventArgs e)
        {
            MainForm.StartGame(true);
        }
    }
}