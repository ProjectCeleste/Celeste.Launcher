#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Celeste_Public_Api.GameScanner.Models;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logger;
using FileInfo = Celeste_Public_Api.GameScanner.Models.FileInfo;

#endregion

namespace Celeste_Public_Api.GameScanner
{
    public class GameScannnerApi
    {
        private readonly object _syncLockCancel = new object();

        private readonly object _syncLockScan = new object();

        public GameScannnerApi(IEnumerable<FileInfo> filesInfo, string filesRootPath)
        {
            if (string.IsNullOrEmpty(filesRootPath))
                throw new ArgumentException("Game files path is null or empty!", nameof(filesRootPath));

            if (!Directory.Exists(filesRootPath))
                Directory.CreateDirectory(filesRootPath);

            if (!filesRootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                filesRootPath += Path.DirectorySeparatorChar;

            FilesInfo = filesInfo;
            FilesRootPath = filesRootPath;
        }

        public IEnumerable<FileInfo> FilesInfo { get; }

        public string FilesRootPath { get; }

        public bool IsScanRunning { get; private set; }

        public bool IsCancellationRequested { get; private set; }

        private CancellationTokenSource Cts { get; set; } = new CancellationTokenSource();

        public static async Task<bool> ScanAndRepairFile(FileInfo fileInfo, string gameFilePath,
            IProgress<ScanAndRepairFileProgress> progress,
            CancellationToken ct)
        {
            try
            {
                var filePath = $"{gameFilePath}{fileInfo.FileName}";

                //#1 File Check
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 1,
                    new ExLog(LogLevel.Info, "-------------------------\r\n" +
                                             $"[{fileInfo.FileName}]\r\n" +
                                             "-------------------------\r\n" +
                                             "      - Checking file...")));

                if (FileCheck.DoFileCheck(filePath, fileInfo.Size, fileInfo.Crc32))
                    goto end;

                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 5,
                    new ExLog(LogLevel.Warn, "          Warning: File is missing or invalid.")));

                ct.ThrowIfCancellationRequested();

                //#6 Download File
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 6,
                    new ExLog(LogLevel.Info, "      - File downloading...")));

                var dowloadProgress = new Progress<DownloadFileProgress>();
                dowloadProgress.ProgressChanged += (o, ea) =>
                {
                    progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName,
                        6 + Convert.ToInt32(Math.Floor((double) ea.ProgressPercentage / 100 * (65 - 6))), ea));
                };
                var tempFileName = Path.GetTempFileName();
                var x = new DownloadFileAsync(new Uri(fileInfo.HttpLink), tempFileName, dowloadProgress);
                await x.DoDownload(ct);

                //#65 Check Downloaded File
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 65,
                    new ExLog(LogLevel.Info, "      - Checking downloaded file...")));

                if (!FileCheck.DoFileCheck(tempFileName, fileInfo.BinSize, fileInfo.BinCrc32))
                {
                    if (File.Exists(tempFileName))
                        File.Delete(tempFileName);

                    throw new Exception("Downloaded file is invalid!");
                }

                //#70 Extract downloaded file
                var tmpFilePath = tempFileName;
                var tempFileName2 = Path.GetTempFileName();
                if (L33TZip.IsL33TZipFile(tempFileName))
                {
                    progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 70,
                        new ExLog(LogLevel.Info, "      - Extract downloaded file...")));

                    var extractProgress = new Progress<L33TZipExtractProgress>();
                    extractProgress.ProgressChanged += (o, ea) =>
                    {
                        progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName,
                            70 + Convert.ToInt32(Math.Floor((double) ea.ProgressPercentage / 100 * (90 - 70))), ea));
                    };
                    await L33TZip.ExtractL33TZipFile(tempFileName, tempFileName2, extractProgress, ct);

                    //#90 Check Downloaded File
                    if (!FileCheck.DoFileCheck(tempFileName2, fileInfo.Size, fileInfo.Crc32))
                    {
                        if (File.Exists(tempFileName))
                            File.Delete(tempFileName);

                        if (File.Exists(tempFileName2))
                            File.Delete(tempFileName2);

                        throw new Exception("Extracted file is invalid!");
                    }

                    tmpFilePath = tempFileName2;
                }

                //#95 Move new file to game folder
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 95,
                    new ExLog(LogLevel.Info, "      - Moving new extracted file...")));

                if (File.Exists(filePath))
                    File.Delete(filePath);

                var pathName = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(pathName) && !Directory.Exists(pathName))
                    Directory.CreateDirectory(pathName);

                File.Move(tmpFilePath, filePath);

                //#99 Removing temporary file
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 99,
                    new ExLog(LogLevel.Info, "      - Clean-up temporary files...")));

                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);

                if (File.Exists(tempFileName2))
                    File.Delete(tempFileName2);

                end:
                //#100
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 100));
            }
            catch (AggregateException e)
            {
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 100,
                    new ExLog(LogLevel.Info, "-------------------------\r\n" +
                                             "!!! Error !!!\r\n" +
                                             "-------------------------\r\n" +
                                             $"{e.Message}")));

                return false;
            }
            return true;
        }

        public static string GetGameFilesRootPath()
        {
            {
                //Custom Path 1
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}Spartan.exe";
                if (File.Exists(path))
                    goto spartanFound;

                //Custom Path 2
                path = $"{AppDomain.CurrentDomain.BaseDirectory}\\AOEO\\Spartan.exe";
                if (File.Exists(path))
                    goto spartanFound;

                //Custom Path 3
                if (Environment.Is64BitOperatingSystem)
                {
                    path =
                        $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\Age Of Empires Online\\Spartan.exe";
                    if (File.Exists(path))
                        goto spartanFound;
                }

                //Custom Path 4
                path =
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Age Of Empires Online\\Spartan.exe";
                if (File.Exists(path))
                    goto spartanFound;

                //Steam 1
                if (Environment.Is64BitOperatingSystem)
                {
                    path =
                        $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\Steam\\steamapps\\common\\Age Of Empires Online\\Spartan.exe";
                    if (File.Exists(path))
                        goto spartanFound;
                }

                //Steam 2
                path =
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Steam\\steamapps\\common\\Age Of Empires Online\\Spartan.exe";
                if (File.Exists(path))
                    goto spartanFound;

                //Original Game Path
                path =
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Local\\Microsoft\\Age of Empires Online\\Spartan.exe";
                if (File.Exists(path))
                    goto spartanFound;

                return $"{AppDomain.CurrentDomain.BaseDirectory}\\AOEO";

                spartanFound:
                return Path.GetDirectoryName(path);
            }
        }

        public void CancelScan()
        {
            if (IsCancellationRequested || !Monitor.TryEnter(_syncLockCancel))
                return;

            try
            {
                if (!IsScanRunning || IsCancellationRequested || Cts.IsCancellationRequested)
                    return;

                IsCancellationRequested = true;
                Cts.Cancel();
            }
            finally
            {
                Monitor.Exit(_syncLockCancel);
            }
        }

        public async Task<bool> FullScanAndRepair(IProgress<ScanAndRepairProgress> progress)
        {
            {
                if (!Monitor.TryEnter(_syncLockScan) || IsScanRunning)
                    throw new Exception("Scan already running!");

                var retVal = false;
                IsScanRunning = true;
                try
                {
                    Cts.Cancel();
                    Cts = new CancellationTokenSource();
                    IsCancellationRequested = false;

                    var t = Task.Run(async () =>
                    {
                        try
                        {
                            var totalCount = FilesInfo.Count();
                            var currentIndex = 0;
                            foreach (var fileInfo in FilesInfo)
                            {
                                currentIndex += 1;

                                Cts.Token.ThrowIfCancellationRequested();

                                progress.Report(new ScanAndRepairProgress(totalCount, currentIndex,
                                    new ExLog(LogLevel.Info, $"{fileInfo.FileName}")));

                                Thread.Sleep(10);

                                var fileProgress = new Progress<ScanAndRepairFileProgress>();
                                var ci = currentIndex;
                                fileProgress.ProgressChanged += (o, ea) =>
                                {
                                    progress.Report(new ScanAndRepairProgress(totalCount, ci, ea));
                                };
                                retVal = await ScanAndRepairFile(fileInfo, FilesRootPath, fileProgress, Cts.Token);

                                Thread.Sleep(10);

                                if (!retVal)
                                    break;
                            }
                        }
                        catch (AggregateException)
                        {
                            retVal = false;
                            throw;
                        }
                    }, Cts.Token);

                    await t;
                }
                catch (AggregateException)
                {
                    retVal = false;
                    throw;
                }
                finally
                {
                    Monitor.Exit(_syncLockScan);
                    IsScanRunning = false;
                }

                return retVal;
            }
        }

        public async Task<bool> QuickScan(IProgress<ScanAndRepairProgress> progress)
        {
            {
                if (!Monitor.TryEnter(_syncLockScan) || IsScanRunning)
                    throw new Exception("Scan already running!");

                var retVal = false;
                IsScanRunning = true;
                try
                {
                    Cts.Cancel();
                    Cts = new CancellationTokenSource();
                    IsCancellationRequested = false;

                    var t = Task.Run(() =>
                    {
                        try
                        {
                            var totalCount = FilesInfo.Count();
                            var currentIndex = 0;
                            foreach (var fileInfo in FilesInfo)
                            {
                                currentIndex += 1;

                                Cts.Token.ThrowIfCancellationRequested();

                                progress.Report(new ScanAndRepairProgress(totalCount, currentIndex,
                                    new ExLog(LogLevel.Info, $"{fileInfo.FileName}")));

                                Thread.Sleep(10);

                                retVal = FileCheck.DoFileQuickCheck($"{FilesRootPath}{fileInfo.FileName}",
                                    fileInfo.Size);

                                Thread.Sleep(10);

                                if (!retVal)
                                    break;
                            }
                        }
                        catch (AggregateException)
                        {
                            retVal = false;
                            throw;
                        }
                    }, Cts.Token);

                    await t;
                }
                catch (AggregateException)
                {
                    retVal = false;
                    throw;
                }
                finally
                {
                    Monitor.Exit(_syncLockScan);
                    IsScanRunning = false;
                }

                return retVal;
            }
        }
    }
}