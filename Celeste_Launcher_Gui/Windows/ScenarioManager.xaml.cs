using Celeste_Launcher_Gui.Services;
using Celeste_Public_Api.Logging;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for ScenarioManager.xaml
    /// </summary>
    public partial class ScenarioManager : Window
    {
        private readonly FileSystemWatcher _folderListener;
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        private readonly string _scenarioDirectoryPath =
            Path.Combine(Environment.GetEnvironmentVariable("userprofile") ?? string.Empty, "Documents", "Spartan",
                "Scenario");

        private readonly object _syncLock = new object();

        public ScenarioManager()
        {
            InitializeComponent();

            if (!Directory.Exists(_scenarioDirectoryPath))
                Directory.CreateDirectory(_scenarioDirectoryPath);

            RefreshList();

            _folderListener = new FileSystemWatcher(_scenarioDirectoryPath, "*.age4scn")
            {
                NotifyFilter =
                    NotifyFilters.CreationTime |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.FileName |
                    NotifyFilters.LastWrite |
                    NotifyFilters.Size
            };
            _folderListener.Changed += FolderListenerEvent;
            _folderListener.Created += FolderListenerEvent;
            _folderListener.Deleted += FolderListenerEvent;
            _folderListener.Renamed += FolderListenerEvent;
            _folderListener.IncludeSubdirectories = true;
            _folderListener.EnableRaisingEvents = true;
        }

        public void RefreshList()
        {
            if (!Monitor.TryEnter(_syncLock))
                return;

            try
            {
                ScenarioListView.Items.Clear();
                foreach (var filePath in Directory.GetFiles(_scenarioDirectoryPath, "*.age4scn", SearchOption.AllDirectories))
                {
                    var txt = filePath.Replace(_scenarioDirectoryPath, string.Empty).Replace(".age4scn", string.Empty);
                    if (txt.StartsWith("/") || txt.StartsWith(@"\"))
                        txt = txt.Substring(1);

                    ScenarioListView.Items.Add(new ListViewItem
                    {
                        Content = txt,
                        Tag = filePath
                    });
                }
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
                Logger.Error(ex, ex.Message);
            }
            finally
            {
                Monitor.Exit(_syncLock);
            }
        }

        private void FolderListenerEvent(object source, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(RefreshList);
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void OnAddScenarioClick(object sender, RoutedEventArgs e)
        {
            _folderListener.EnableRaisingEvents = false;

            try
            {
                using (var dlg = new System.Windows.Forms.OpenFileDialog
                {
                    Filter = $"{Properties.Resources.ScenarioEditorFilePickerFileTypes} (*.age4scn)|*.age4scn",
                    CheckFileExists = true,
                    Title = Properties.Resources.ScenarioEditorFilePickerTitle,
                    Multiselect = true
                })
                {
                    if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    foreach (var filename in dlg.FileNames)
                    {
                        var selectedDestinationPath =
                            Path.Combine(_scenarioDirectoryPath, Path.GetFileName(filename) ?? string.Empty);

                        if (File.Exists(selectedDestinationPath))
                        {
                            var userSelectedToOverwrite = GenericMessageDialog.Show(
                                string.Format(Properties.Resources.ScenarioEditorOverwritePrompt, selectedDestinationPath),
                                DialogIcon.None,
                                DialogOptions.YesNo);

                            if (userSelectedToOverwrite != true)
                                return;
                        }

                        File.Copy(filename, selectedDestinationPath, true);
                    }

                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }
            finally
            {
                _folderListener.EnableRaisingEvents = true;
            }
        }

        private void OnRemoveScenarioClick(object sender, RoutedEventArgs e)
        {
            _folderListener.EnableRaisingEvents = false;
            try
            {
                if (ScenarioListView.SelectedItems.Count < 1)
                    return;

                foreach (ListViewItem lvi in ScenarioListView.SelectedItems)
                {
                    if (!File.Exists((string)lvi.Tag))
                        continue;

                    var userSelectedToDeleteFile = GenericMessageDialog.Show(
                        string.Format(Properties.Resources.ScenarioEditorDeleteScenarioPrompt, lvi.Content),
                        DialogIcon.None,
                        DialogOptions.YesNo);

                    if (userSelectedToDeleteFile == true)
                        File.Delete((string)lvi.Tag);
                }

                RefreshList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }
            finally
            {
                _folderListener.EnableRaisingEvents = true;
            }
        }

        private void OnScenarioDirectoryClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(_scenarioDirectoryPath))
                Process.Start(_scenarioDirectoryPath);
        }

        private void OnDownloadScenariosClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/resources/categories/custom-scenario.2/");
        }

        private void OnHelpClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/wiki/offline-mode/");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _folderListener.Dispose();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await GameService.StartGame(true);
        }
    }
}
