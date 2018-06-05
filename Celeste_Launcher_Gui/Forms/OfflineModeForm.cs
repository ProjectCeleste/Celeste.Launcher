﻿#region Using directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.Helpers;
#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class OfflineModeForm : Form
    {
        private readonly GameScannnerApi _gameScannner;
        public bool internetAccess;
        public OfflineModeForm()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    internetAccess = true;
                }
            }
            catch
            {
                internetAccess = false;
            }
            
            if (internetAccess == true)
            {
                var path = !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath)
                ? Program.UserConfig.GameFilesPath
                : GameScannnerApi.GetGameFilesRootPath();
            
                _gameScannner = GameScannnerApi.InstallGameEditor(Program.UserConfig.IsSteamVersion,
                    Program.UserConfig.IsLegacyXLive,
                    path);
            }
            InitializeComponent();

        }

        private async void EditorForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.UserConfig.GameFilesPath) || !File.Exists(Program.UserConfig.GameFilesPath + "\\Spartan.exe"))
            {
                MsgBox.ShowMessage("Please run a game scan before using the editor or offline mode.");
                Close();
                MainForm mf = new MainForm();
                mf.ToolStripMenuItem1_Click(sender, e);
                goto end;
            }
            if (!Directory.Exists(Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario"))
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario");
            }
            
            comboBox1.SelectedIndex = 0;
            editorFolderListener();
            playFolderListener();
            refreshLists();
            editorWatcher.EnableRaisingEvents = true;
            playWatcher.EnableRaisingEvents = true;


            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            if (internetAccess == true)
            {
                if (await _gameScannner.QuickScan())
                {
                    Btn_Install_Editor.Enabled = false;
                    btn_Editor.Enabled = true;

                    label2.Text = @"✓";
                    label2.ForeColor = Color.Green;
                }
                else
                {
                    label2.Text = @"✕";
                    label2.ForeColor = Color.Red;

                    Btn_Install_Editor.Enabled = true;
                    btn_Editor.Enabled = false;
                }
            }
            else
            {
                Btn_Install_Editor.Enabled = true;
                btn_Editor.Enabled = true;

                label2.Text = @"?";
                label2.ForeColor = Color.Black;
            }
            end:;
        }


        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            editorWatcher.EnableRaisingEvents = false;
            playWatcher.EnableRaisingEvents = false;
            Close();
        }

        private void Btn_Browse_Click(object sender, EventArgs e) // Editor Button Click Event (Launch Editor)
        {
            btn_Editor.Enabled = false;
            try
            {
                btn_Editor.Enabled = false;


                //Launch Game
                //var path = !string.IsNullOrWhiteSpace(Program.UserConfig.GameFilesPath)
                //    ? Program.UserConfig.GameFilesPath
                //    : GameScannnerApi.GetGameFilesRootPath();

                var path = Program.UserConfig.GameFilesPath;

                var spartanPath = Path.Combine(path, "Editor.exe");

                if (!File.Exists(spartanPath))
                    throw new FileNotFoundException("Editor.exe not found!", spartanPath);

                //isSteam
                if (!Program.UserConfig.IsSteamVersion)
                {
                    var steamApiDll = Path.Combine(Program.UserConfig.GameFilesPath, "steam_api.dll");
                    if (File.Exists(steamApiDll))
                        File.Delete(steamApiDll);
                }

                //
                string lang;
                switch (Program.UserConfig.GameLanguage)
                {
                    case GameLanguage.deDE:
                        lang = "de-DE";
                        break;
                    case GameLanguage.enUS:
                        lang = "en-US";
                        break;
                    case GameLanguage.esES:
                        lang = "es-ES";
                        break;
                    case GameLanguage.frFR:
                        lang = "fr-FR";
                        break;
                    case GameLanguage.itIT:
                        lang = "it-IT";
                        break;
                    case GameLanguage.zhCHT:
                        lang = "zh-CHT";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Program.UserConfig.GameLanguage),
                            Program.UserConfig.GameLanguage, null);
                }

                Process.Start(
                    new ProcessStartInfo(spartanPath, $"LauncherLang={lang} LauncherLocale=1033")
                    {
                        WorkingDirectory = path
                    });
                
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_Editor.Enabled = true;
        }


        private void Btn_Install_Editor_Click(object sender, EventArgs e)
        {
            Btn_Install_Editor.Enabled = false;
            btn_Editor.Enabled = false;
            if (internetAccess == true)
            {
                try
                {
                    using (var form = new GameScanProgressForm(_gameScannner))
                    {
                        form.ShowDialog();

                        if (form.DialogResult != DialogResult.OK)
                            throw new Exception("Installation failed");

                        label2.Text = @"OK";
                        label2.ForeColor = Color.Green;

                        btn_Editor.Enabled = true;
                    }
                }
                catch (Exception exception)
                {
                    MsgBox.ShowMessage(
                        $"Error: Error during the installation of the 'Game Editor'. Error message: {exception.Message}",
                        @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    label2.Text = @"Missing";
                    label2.ForeColor = Color.Red;

                    Btn_Install_Editor.Enabled = true;
                }
            } else
            {
                MsgBox.ShowMessage("Internet Connection is not available. Functions such as editor installation are limited.");
            }
            Btn_Install_Editor.Enabled = true;
            btn_Editor.Enabled = true;
        }

        public void btnOfflineLaunch(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.offlineLaunch();
        }

        public void refreshLists()
        {
            String editorScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
            String playScenPath = Program.UserConfig.GameFilesPath + "\\Scenario";
            
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            
            string filepath1 = playScenPath;
            DirectoryInfo d1 = new DirectoryInfo(filepath1);
            string filepath2 = editorScenPath;
            DirectoryInfo d2 = new DirectoryInfo(filepath2);

            if (Directory.Exists(Program.UserConfig.GameFilesPath) && File.Exists(Program.UserConfig.GameFilesPath + "\\Spartan.exe"))
            {
                try
                {
                    foreach (var file in d1.GetFiles("*.age4scn"))
                    {
                        listBox1.Items.Add(Path.GetFileNameWithoutExtension(file.FullName.ToString()));
                    }

                    foreach (var file in d2.GetFiles("*.age4scn"))
                    {
                        listBox2.Items.Add(Path.GetFileNameWithoutExtension(file.FullName.ToString()));
                    }
                }
                catch (Exception err)
                {
                    MsgBox.ShowMessage(
                    $"Warning: Error during quick scan. Error message: {err.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MsgBox.ShowMessage("Please run a Game Scan first");
                Close();
            }
        }

        public void btnRefresh(object sender, EventArgs e)
        {
            refreshLists();
        }

        private void btnShow1_Click(object sender, EventArgs e)
        {
            String playScenPath = Program.UserConfig.GameFilesPath + "\\Scenario";
            Process.Start(playScenPath);
        }

        private void btnShow2_Click(object sender, EventArgs e)
        {
            String editorScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
            Process.Start(editorScenPath);
        }
        
        private void importToEditor(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Scenario files (*.age4scn)|*.age4scn",
                CheckFileExists = true,
                Title = @"Choose Scenario File",
                Multiselect = false
            };
            dlg.ShowDialog();
            if (dlg.FileName == string.Empty)
            {
                Console.WriteLine("No file was chosen, closing OpenFileDialog");
            }
            else
            {
                try
                {
                    String editorScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
                    String selectedFullPath = dlg.FileName;
                    String selectedFileOnly = Path.GetFileName(selectedFullPath);
                    String selectedDestinationPath = editorScenPath + "\\" + selectedFileOnly;
                    File.Move(selectedFullPath, selectedDestinationPath);
                    refreshLists();
                } catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }
            refreshLists();
        }

        private void importToOfflinePlayer(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Scenario files (*.age4scn)|*.age4scn",
                CheckFileExists = true,
                Title = @"Choose Scenario File",
                Multiselect = false
            };
            dlg.ShowDialog();
            if (dlg.FileName == string.Empty)
            {
                Console.WriteLine("No file was chosen, closing OpenFileDialog");
            }
            else
            {
                try
                {
                    String playScenPath = Program.UserConfig.GameFilesPath + "\\Scenario";
                    String selectedFullPath = dlg.FileName;
                    String selectedFileOnly = Path.GetFileName(selectedFullPath);
                    String selectedDestinationPath = playScenPath + "\\" + selectedFileOnly;
                    File.Move(selectedFullPath, selectedDestinationPath);
                    refreshLists();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }
            refreshLists();
        }

        private void moveScen(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox2.SelectedIndex == -1)
            {
                string selectedFile = listBox1.GetItemText(listBox1.SelectedItem);
                String editorScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
                String playScenPath = Program.UserConfig.GameFilesPath + "\\Scenario";
                String location = playScenPath + "\\" + selectedFile + ".age4scn";
                String destination = editorScenPath + "\\" + selectedFile + ".age4scn";
                string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                try
                {
                    if (File.Exists(destination))
                    {
                        var msg = new MsgBoxYesNo("File already exists in the new location. Replace?");
                        var msg2 = msg.ShowDialog();
                        if (msg2 == DialogResult.OK)
                        {
                            if (selectedItem == "Copy")
                            {
                                File.Delete(destination);
                                File.Copy(location, destination);
                                refreshLists();
                            }
                            else
                            {
                                File.Delete(destination);
                                File.Move(location, destination);
                                refreshLists();
                            }
                        }
                        else
                        {
                            goto end;
                        }

                    }
                    else
                    {
                        if (selectedItem == "Copy")
                        {
                            File.Delete(destination);
                            File.Copy(location, destination);
                            refreshLists();
                        }
                        else
                        {
                            File.Delete(destination);
                            File.Move(location, destination);
                            refreshLists();
                        }
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            } else if (listBox1.SelectedIndex == -1 && listBox2.SelectedIndex != -1)
            {
                string selectedFile = listBox2.GetItemText(listBox2.SelectedItem);
                String editorScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
                String playScenPath = Program.UserConfig.GameFilesPath + "\\Scenario";
                String location = editorScenPath + "\\" + selectedFile + ".age4scn";
                String destination = playScenPath + "\\" + selectedFile + ".age4scn";
                string selectedItem = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                try
                {
                    if (File.Exists(destination))
                    {
                        var msg = new MsgBoxYesNo("File already exists in the new location. Replace?");
                        var msg2 = msg.ShowDialog();
                        if (msg2 == DialogResult.OK)
                        {
                            if (selectedItem == "Copy")
                            {
                                File.Delete(destination);
                                File.Copy(location, destination);
                                refreshLists();
                            }
                            else
                            {
                                File.Delete(destination);
                                File.Move(location, destination);
                                refreshLists();
                            }
                        }
                        else
                        {
                            goto end;
                        }

                    }
                    else
                    {
                        if (selectedItem == "Copy")
                        {
                            File.Delete(destination);
                            File.Copy(location, destination);
                            refreshLists();
                        }
                        else
                        {
                            File.Delete(destination);
                            File.Move(location, destination);
                            refreshLists();
                        }
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }
         end:;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/threads/read-me.3428/");
        }

        private void clearListBox1(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
        }

        private void clearListBox2(object sender, EventArgs e)
        {
            listBox2.ClearSelected();
        }

        private void btnDlMore_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/forums/custom-scenario-sharing.79/");
        }

        private void btnSync(object sender, EventArgs e)
        {
            var msg = new MsgBoxYesNo("This will combine both folders with eachother. Any duplicate files will be replaced with the version from the Editor's Folder. Continue?");
            var msg2 = msg.ShowDialog();
            if (msg2 == DialogResult.OK)
            {
                String editorScenPath = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
                String playScenPath = Program.UserConfig.GameFilesPath + "\\Scenario";
                DirectoryInfo d1 = new DirectoryInfo(playScenPath);
                DirectoryInfo d2 = new DirectoryInfo(editorScenPath);
                List<string> usedList = new List<string>();
                try
                {
                    foreach (var file in d2.GetFiles("*.age4scn")) // List 2 Files
                    {
                        string filePath = editorScenPath + "\\" + file.Name.ToString();
                        string destinationPath = playScenPath + "\\" + file.Name.ToString();
                        File.Copy(filePath, destinationPath, true);
                        usedList.Add(file.Name.ToString());
                    }

                    foreach (var file in d1.GetFiles("*.age4scn")) // List 1 Files
                    {
                        string filePath = playScenPath + "\\" + file.Name.ToString();
                        string destinationPath = editorScenPath + "\\" + file.Name.ToString();
                        File.Copy(filePath, destinationPath, true);
                        if (usedList.Contains(file.Name.ToString()))
                        {
                            Console.WriteLine("Skipping File in Sync: " + file.Name.ToString());
                        }
                        else
                        {
                            File.Copy(filePath, destinationPath, true);
                        }
                    }
                }
                catch (Exception err)
                {
                    MsgBox.ShowMessage(
                    $"Warning: Error during quick scan. Error message: {err.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                refreshLists();
                MsgBox.ShowMessage("Folders were synced!");
            }
        }

        public void editorFolderListener()
        {
            editorWatcher.Path = Environment.GetEnvironmentVariable("userprofile") + "\\Documents\\Age of Empires Online\\Scenario";
            editorWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            editorWatcher.Filter = "*.*";
            editorWatcher.Changed += new FileSystemEventHandler(OnChangedEditor);
            editorWatcher.Created += new FileSystemEventHandler(OnChangedEditor);
            editorWatcher.Deleted += new FileSystemEventHandler(OnChangedEditor);
            editorWatcher.Renamed += new RenamedEventHandler(OnChangedEditor);
            editorWatcher.EnableRaisingEvents = true;
            editorWatcher.IncludeSubdirectories = true;
        }

        public void playFolderListener()
        {
            playWatcher.Path = Program.UserConfig.GameFilesPath + "\\Scenario";
            playWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            playWatcher.Filter = "*.*";
            playWatcher.Changed += new FileSystemEventHandler(OnChangedPlay);
            playWatcher.Created += new FileSystemEventHandler(OnChangedPlay);
            playWatcher.Deleted += new FileSystemEventHandler(OnChangedPlay);
            playWatcher.Renamed += new RenamedEventHandler(OnChangedPlay);
            playWatcher.EnableRaisingEvents = true;
            playWatcher.IncludeSubdirectories = true;
        }

        private void OnChangedEditor(object source, FileSystemEventArgs e)
        {
            try
            {
                refreshLists();
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Disposed Object Caught!");
            }
        }

        private void OnChangedPlay(object source, FileSystemEventArgs e)
        {
            try
            {
                refreshLists();
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Disposed Object Caught!");
            }
        }
        
    }
}