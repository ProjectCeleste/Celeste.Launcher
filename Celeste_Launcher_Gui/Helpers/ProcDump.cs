#region Using directives

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using ProjectCeleste.GameFiles.GameScanner.FileDownloader;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ProcDump
    {
        public static async Task DoDownloadAndInstallProcDump(IProgress<int> progress = null,
            CancellationToken ct = default(CancellationToken))
        {
            //Download File
            progress?.Report(5);

            const string downloadLink = @"https://download.sysinternals.com/files/Procdump.zip";

            var tempFileName = Path.GetTempFileName();

            try
            {
                var downloadFileAsync = new SimpleFileDownloader(downloadLink, tempFileName);
                if (progress != null)
                    downloadFileAsync.ProgressChanged += (sender, args) =>
                    {
                        progress.Report(Convert.ToInt32(Math.Floor(70 * (downloadFileAsync.DownloadProgress / 100))));
                    };
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
                extractProgress.ProgressChanged += (o, ea) =>
                {
                    progress.Report(70 + Convert.ToInt32(Math.Floor(20 * (ea / 100))));
                };
            }
            var tempDir = Path.Combine(Path.GetTempPath(), "Celeste_Launcher_ProcDump");

            if (Directory.Exists(tempDir))
                Misc.CleanUpFiles(tempDir, "*.*");

            try
            {
                await ZipUtils.ExtractZipFile(tempFileName, tempDir, ct, null, extractProgress);
            }
            catch (AggregateException)
            {
                Misc.CleanUpFiles(tempDir, "*.*");
                throw;
            }
            finally
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);
            }

            //Move File
            progress?.Report(90);

            var destinationDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            try
            {
                Misc.MoveFiles(tempDir, destinationDir);
            }
            finally
            {
                Misc.CleanUpFiles(tempDir, "*.*");
            }

            //
            progress?.Report(100);
        }
    }
}