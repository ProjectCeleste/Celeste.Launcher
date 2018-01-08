#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Celeste_Public_Api.GameScanner_Api.Models;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.GameScanner_Api
{
    public class GameScannnerApi
    {
        private CancellationTokenSource _cts;

        public GameScannnerApi(string filesRootPath, bool isSteam, bool betaUpdate)
        {
            if (string.IsNullOrEmpty(filesRootPath))
                throw new ArgumentException(@"Game files path is null or empty!", nameof(filesRootPath));

            if (!Directory.Exists(filesRootPath))
                Directory.CreateDirectory(filesRootPath);

            if (!filesRootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                filesRootPath += Path.DirectorySeparatorChar;

            FilesInfo = GetGameFilesInfo(betaUpdate, isSteam);
            FilesInfo = GetGameFilesInfo(isSteam, betaUpdate);
            FilesRootPath = filesRootPath;
            _cts = new CancellationTokenSource();
        }

        public GameScannnerApi(IEnumerable<GameFileInfo> filesInfo, string filesRootPath)
        {
            if (string.IsNullOrEmpty(filesRootPath))
                throw new ArgumentException(@"Game files path is null or empty!", nameof(filesRootPath));

            if (!Directory.Exists(filesRootPath))
                Directory.CreateDirectory(filesRootPath);

            if (!filesRootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                filesRootPath += Path.DirectorySeparatorChar;

            FilesInfo = filesInfo;
            FilesRootPath = filesRootPath;
            _cts = new CancellationTokenSource();
        }
          
        public IEnumerable<GameFileInfo> FilesInfo { get; }

        public string FilesRootPath { get; }

        public bool IsScanRunning { get; private set; }

        public bool IsCancellationRequested { get; private set; }

        private static bool RunFileCheck(string filePath, long fileSize, uint fileCrc32)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length == fileSize &&
                   Crc32Utils.RunCrc32FileCheck(filePath, fileCrc32);
        }

        public static bool RunFileQuickCheck(string filePath, long fileSize)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length == fileSize;
        }

        private static async Task<bool> ScanAndRepairFile(GameFileInfo fileInfo, string gameFilePath,
            IProgress<ScanAndRepairFileProgress> progress,
            CancellationToken ct)
        {
            try
            {
                var filePath = !string.IsNullOrEmpty(fileInfo.OverrideFileName)
                    ? $"{gameFilePath}{fileInfo.OverrideFileName}"
                    : $"{gameFilePath}{fileInfo.FileName}";

                //#1 File Check
                var filenameui = !string.IsNullOrEmpty(fileInfo.OverrideFileName)
                    ? fileInfo.OverrideFileName
                    : fileInfo.FileName;

                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 1,
                    new ExLog(LogLevel.Info, "-------------------------\r\n" +
                                             $"[{filenameui}]\r\n" +
                                             "-------------------------\r\n" +
                                             "      - Checking file...")));

                if (RunFileCheck(filePath, fileInfo.Size, fileInfo.Crc32))
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
                var x = new DownloadFileUtils(new Uri(fileInfo.HttpLink), tempFileName, dowloadProgress);
                await x.DoDownload(ct);

                //#65 Check Downloaded File
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 65,
                    new ExLog(LogLevel.Info, "      - Checking downloaded file...")));

                if (!RunFileCheck(tempFileName, fileInfo.BinSize, fileInfo.BinCrc32))
                {
                    if (File.Exists(tempFileName))
                        File.Delete(tempFileName);

                    throw new Exception("Downloaded file is invalid!");
                }

                //#70 Extract downloaded file
                var tmpFilePath = tempFileName;
                var tempFileName2 = Path.GetTempFileName();
                if (ZipUtils.IsL33TZipFile(tempFileName))
                {
                    progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 70,
                        new ExLog(LogLevel.Info, "      - Extract downloaded file...")));

                    var extractProgress = new Progress<ZipFileProgress>();
                    extractProgress.ProgressChanged += (o, ea) =>
                    {
                        progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName,
                            70 + Convert.ToInt32(Math.Floor((double) ea.ProgressPercentage / 100 * (90 - 70))), ea));
                    };
                    await ZipUtils.DoExtractL33TZipFile(tempFileName, tempFileName2, extractProgress, ct);

                    //#90 Check Downloaded File
                    if (!RunFileCheck(tempFileName2, fileInfo.Size, fileInfo.Crc32))
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
                    new ExLog(LogLevel.Info, "      - Moving new file...")));

                if (File.Exists(filePath))
                    File.Delete(filePath);

                var pathName = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(pathName) && !Directory.Exists(pathName))
                    Directory.CreateDirectory(pathName);

                File.Move(tmpFilePath, filePath);

                //#99 Removing temporary file
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 99,
                    new ExLog(LogLevel.Info, "      - Clean-up temporary files...")));

                if (File.Exists(tmpFilePath))
                    File.Delete(tmpFilePath);

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
            if (IsCancellationRequested)
                return;

            if (!IsScanRunning)
                return;

            IsCancellationRequested = true;

            if (!_cts.IsCancellationRequested)
                _cts.Cancel();
        }

        public async Task<bool> ScanAndRepair(IProgress<ScanAndRepairProgress> progress)
        {
            {
                if (IsScanRunning)
                    throw new Exception("Scan already running!");

                var retVal = false;
                IsScanRunning = true;
                try
                {
                    _cts.Cancel();
                    _cts = new CancellationTokenSource();
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

                                _cts.Token.ThrowIfCancellationRequested();

                                progress.Report(new ScanAndRepairProgress(totalCount, currentIndex,
                                    new ExLog(LogLevel.Info, $"{fileInfo.FileName}")));

                                await Task.Delay(10).ConfigureAwait(false);

                                var fileProgress = new Progress<ScanAndRepairFileProgress>();
                                var ci = currentIndex;
                                fileProgress.ProgressChanged += (o, ea) =>
                                {
                                    progress.Report(new ScanAndRepairProgress(totalCount, ci, ea));
                                };
                                retVal = await ScanAndRepairFile(fileInfo, FilesRootPath, fileProgress, _cts.Token);

                                await Task.Delay(10).ConfigureAwait(false);

                                if (!retVal)
                                    break;
                            }
                        }
                        catch (AggregateException)
                        {
                            retVal = false;
                            throw;
                        }
                        finally
                        {
                            IsScanRunning = false;
                        }
                    }, _cts.Token);

                    await t;
                }
                finally
                {
                    IsScanRunning = false;
                }

                return retVal;
            }
        }

        public async Task<bool> QuickScan(IProgress<ScanAndRepairProgress> progress)
        {
            {
                if (IsScanRunning)
                    throw new Exception("Scan already running!");

                var retVal = false;
                IsScanRunning = true;
                try
                {
                    _cts.Cancel();
                    _cts = new CancellationTokenSource();
                    IsCancellationRequested = false;

                    var totalCount = FilesInfo.Count();
                    var currentIndex = 0;
                    foreach (var fileInfo in FilesInfo)
                    {
                        currentIndex += 1;

                        _cts.Token.ThrowIfCancellationRequested();

                        progress.Report(new ScanAndRepairProgress(totalCount, currentIndex,
                            new ExLog(LogLevel.Info, $"{fileInfo.FileName}")));

                        await Task.Delay(10).ConfigureAwait(false);

                        retVal = RunFileQuickCheck($"{FilesRootPath}{fileInfo.FileName}",
                            fileInfo.Size);

                        await Task.Delay(10).ConfigureAwait(false);

                        if (!retVal)
                            break;
                    }
                }
                finally
                {
                    IsScanRunning = false;
                }

                return retVal;
            }
        }

        public static IEnumerable<GameFileInfo> GetGameFilesInfo(bool isSteam, bool betaUpdate)
        {
            var filesInfo = new GameFilesInfo();

            //Load default manifest
            foreach (var fileInfo in FilesInfoFromGameManifest("production", 6148, isSteam))
                if (filesInfo.FileInfo.ContainsKey(fileInfo.FileName.ToLower()))
                    filesInfo.FileInfo[fileInfo.FileName.ToLower()] = fileInfo;
                else
                    filesInfo.FileInfo.Add(fileInfo.FileName.ToLower(), fileInfo);

            //Override for celeste file
            foreach (var fileInfo in FilesInfoOverrideFromCelesteXml(false))
                if (filesInfo.FileInfo.ContainsKey(fileInfo.FileName.ToLower()))
                    filesInfo.FileInfo[fileInfo.FileName.ToLower()] = fileInfo;
                else
                    filesInfo.FileInfo.Add(fileInfo.FileName.ToLower(), fileInfo);

            if (!betaUpdate)
                return filesInfo.FileInfo.Values;

            //Override for celeste file (beta)
            try
            {
                foreach (var fileInfo in FilesInfoOverrideFromCelesteXml(true))
                    if (filesInfo.FileInfo.ContainsKey(fileInfo.FileName.ToLower()))
                        filesInfo.FileInfo[fileInfo.FileName.ToLower()] = fileInfo;
                    else
                        filesInfo.FileInfo.Add(fileInfo.FileName.ToLower(), fileInfo);
            }
            catch (Exception)
            {
                //Better to ignore any error for this one!
            }

            return filesInfo.FileInfo.Values;
        }

        public static IEnumerable<GameFileInfo> FilesInfoFromGameManifest(string type, int build, bool isSteam)
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile($"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/manifest.txt",
                    tempFileName);
            }

            var retVal = from line in File.ReadAllLines(tempFileName)
                where line.StartsWith("+")
                where
                      // Launcher
                      !line.StartsWith("+AoeOnlineDlg.dll") && !line.StartsWith("+AoeOnlinePatch.dll") &&
                      !line.StartsWith("+expapply.dll") && !line.StartsWith("+LauncherLocList.txt") &&
                      !line.StartsWith("+LauncherStrings-de-DE.xml") &&
                      !line.StartsWith("+LauncherStrings-en-US.xml") &&
                      !line.StartsWith("+LauncherStrings-es-ES.xml") &&
                      !line.StartsWith("+LauncherStrings-fr-FR.xml") &&
                      !line.StartsWith("+LauncherStrings-it-IT.xml") &&
                      !line.StartsWith("+LauncherStrings-zh-CHT.xml") && !line.StartsWith("+AOEOnline.exe.cfg") &&
                      //Beta Launcher
                      !line.StartsWith("+Launcher.exe") &&
                      !line.StartsWith("+LauncherReplace.exe") &&
                      !line.StartsWith("+LauncherLocList.txt") &&
                      !line.StartsWith("+AOEO_Privacy.rtf") &&
                      !line.StartsWith("+pw32b.dll") &&
                      //Steam
                      (!line.StartsWith("+steam_api.dll") || isSteam && line.StartsWith("+steam_api.dll")) &&
                      //Junk
                      !line.StartsWith("+t3656t4234.tmp")
                select line.Split('|')
                into lineSplit
                select new GameFileInfo
                {
                    FileName = lineSplit[0].Substring(1, lineSplit[0].Length - 1).ToLower(),
                    OverrideFileName = null,
                    Crc32 = Convert.ToUInt32(lineSplit[1]),
                    Size = Convert.ToInt64(lineSplit[2]),
                    HttpLink = $"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/{lineSplit[3]}",
                    BinCrc32 = Convert.ToUInt32(lineSplit[4]),
                    BinSize = Convert.ToInt64(lineSplit[5])
                };

            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            return retVal;
        }

        private static IEnumerable<GameFileInfo> FilesInfoOverrideFromCelesteXml(bool betaUpdate)
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile(
                    betaUpdate
                        ? "https://downloads.projectceleste.com/game_files/manifest_override_b.xml"
                        : "https://downloads.projectceleste.com/game_files/manifest_override.xml",
                    tempFileName);
            }

            var retVal = XmlUtils.DeserializeFromFile<GameFilesInfo>(tempFileName).FileInfo.Values;

            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            return retVal;
        }
    }
}