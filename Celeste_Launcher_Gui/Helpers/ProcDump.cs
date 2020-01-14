#region Using directives

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ProjectCeleste.GameFiles.GameScanner.FileDownloader;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ProcDump
    {
        public static async Task DoDownloadAndInstallProcDump(IProgress<int> progress = null,
            CancellationToken ct = default)
        {
            //Download File
            progress?.Report(5);

            const string downloadLink = "https://download.sysinternals.com/files/Procdump.zip";

            string tempFileName = Path.GetTempFileName();

            try
            {
                SimpleFileDownloader downloadFileAsync = new SimpleFileDownloader(downloadLink, tempFileName);
                if (progress != null)
                {
                    downloadFileAsync.ProgressChanged += (sender, args) => progress.Report(Convert.ToInt32(Math.Floor(70 * (downloadFileAsync.DownloadProgress / 100))));
                }

                await downloadFileAsync.DownloadAsync(ct);
            }
            catch (AggregateException)
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);

                throw;
            }

            //Extract File
            progress?.Report(65);

            Progress<double> extractProgress = null;
            if (progress != null)
            {
                extractProgress = new Progress<double>();
                extractProgress.ProgressChanged += (_, ea) => progress.Report(70 + Convert.ToInt32(Math.Floor(20 * (ea / 100))));
            }
            string tempDir = Path.Combine(Path.GetTempPath(), "Celeste_Launcher_ProcDump");

            if (Directory.Exists(tempDir))
                Files.CleanUpFiles(tempDir, "*.*");

            try
            {
                await ZipUtils.ExtractZipFile(tempFileName, tempDir, extractProgress, ct);
            }
            catch (AggregateException)
            {
                Files.CleanUpFiles(tempDir, "*.*");
                throw;
            }
            finally
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);
            }

            //Move File
            progress?.Report(90);

            string destinationDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            try
            {
                Files.MoveFiles(tempDir, destinationDir);
            }
            finally
            {
                Files.CleanUpFiles(tempDir, "*.*");
            }

            //
            progress?.Report(100);
        }
    }
}