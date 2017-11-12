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
using Celeste_Public_Api.Logger;
using FileInfo = Celeste_Public_Api.GameScanner_Api.Models.FileInfo;

#endregion

namespace Celeste_Public_Api.GameScanner_Api
{
    public class GameScannnerApi
    {
        public GameScannnerApi(bool betaUpdate, string filesRootPath)
        {
            if (string.IsNullOrEmpty(filesRootPath))
                throw new ArgumentException(@"Game files path is null or empty!", nameof(filesRootPath));

            if (!Directory.Exists(filesRootPath))
                Directory.CreateDirectory(filesRootPath);

            if (!filesRootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                filesRootPath += Path.DirectorySeparatorChar;

            FilesInfo = GetGameFilesInfo(betaUpdate);
            FilesRootPath = filesRootPath;
        }

        public IEnumerable<FileInfo> FilesInfo { get; }

        public string FilesRootPath { get; }

        public bool IsScanRunning { get; private set; }

        public bool IsCancellationRequested { get; private set; }

        private CancellationTokenSource Cts { get; set; } = new CancellationTokenSource();

        public static bool RunFileCheck(string filePath, long fileSize, uint fileCrc32)
        {
            return File.Exists(filePath) && new System.IO.FileInfo(filePath).Length == fileSize &&
                   Crc32Utils.RunCrc32FileCheck(filePath, fileCrc32);
        }

        public static bool RunFileQuickCheck(string filePath, long fileSize)
        {
            return File.Exists(filePath) && new System.IO.FileInfo(filePath).Length == fileSize;
        }

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
            if (IsCancellationRequested)
                return;

            if (!IsScanRunning)
                return;

            IsCancellationRequested = true;

            if (!Cts.IsCancellationRequested)
                Cts.Cancel();
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

                                await Task.Delay(10).ConfigureAwait(false);

                                var fileProgress = new Progress<ScanAndRepairFileProgress>();
                                var ci = currentIndex;
                                fileProgress.ProgressChanged += (o, ea) =>
                                {
                                    progress.Report(new ScanAndRepairProgress(totalCount, ci, ea));
                                };
                                retVal = await ScanAndRepairFile(fileInfo, FilesRootPath, fileProgress, Cts.Token);

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
                    }, Cts.Token);

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
                    Cts.Cancel();
                    Cts = new CancellationTokenSource();
                    IsCancellationRequested = false;

                    var totalCount = FilesInfo.Count();
                    var currentIndex = 0;
                    foreach (var fileInfo in FilesInfo)
                    {
                        currentIndex += 1;

                        Cts.Token.ThrowIfCancellationRequested();

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

        public static IEnumerable<FileInfo> GetGameFilesInfo(bool betaUpdate, string type = "production",
            int build = 6148)
        {
            var filesInfo = new FilesInfo();

            //Load default manifest
            foreach (var fileInfo in FilesInfoFromGameManifest(type, build))
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

        private static IEnumerable<FileInfo> FilesInfoFromGameManifest(string type, int build)
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile($"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/manifest.txt",
                    tempFileName);
            }

            var retVal = from line in File.ReadAllLines(tempFileName)
                where line.StartsWith("+")
                where !line.StartsWith("+AoeOnlineDlg.dll") && !line.StartsWith("+AoeOnlinePatch.dll") &&
                      !line.StartsWith("+expapply.dll") && !line.StartsWith("+LauncherLocList.txt") &&
                      !line.StartsWith("+LauncherStrings-de-DE.xml") &&
                      !line.StartsWith("+LauncherStrings-en-US.xml") &&
                      !line.StartsWith("+LauncherStrings-es-ES.xml") &&
                      !line.StartsWith("+LauncherStrings-fr-FR.xml") &&
                      !line.StartsWith("+LauncherStrings-it-IT.xml") &&
                      !line.StartsWith("+LauncherStrings-zh-CHT.xml") && !line.StartsWith("+AOEOnline.exe.cfg") &&
                      !line.StartsWith("+steam_api.dll") && !line.StartsWith("+t3656t4234.tmp")
                select line.Split('|')
                into lineSplit
                select new FileInfo
                {
                    FileName = lineSplit[0].Substring(1, lineSplit[0].Length - 1),
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

        private static IEnumerable<FileInfo> FilesInfoOverrideFromCelesteXml(bool betaUpdate)
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

            var retVal = XmlUtils.DeserializeFromFile<FilesInfo>(tempFileName).FileInfo.Values;

            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            return retVal;
        }

        public static async Task NewOverrideFilesInfo(string inputFolder, string inputFolderB, string outputFolder,
            string baseHttpLink,
            IProgress<ZipFileProgress> progress,
            CancellationToken ct)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            //
            if (!outputFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                outputFolder += Path.DirectorySeparatorChar;

            //Load default manifest
            var defaultFilesInfo = new FilesInfo();
            foreach (var fileInfo in FilesInfoFromGameManifest("production", 6148))
                if (!defaultFilesInfo.FileInfo.ContainsKey(fileInfo.FileName.ToLower()))
                    defaultFilesInfo.FileInfo.Add(fileInfo.FileName.ToLower(), fileInfo);

            //
            var newFilesInfo = await GenerateOverrideFilesInfo(defaultFilesInfo, inputFolder,
                $"{outputFolder}bin_override", baseHttpLink + "bin_override/", progress, ct);

            newFilesInfo.ToXml($"{outputFolder}manifest_override.xml");

            //
            if (string.IsNullOrEmpty(inputFolderB))
                return;

            //Update default manifest with override
            foreach (var fileInfo in newFilesInfo.FileInfo.Values)
                if (defaultFilesInfo.FileInfo.ContainsKey(fileInfo.FileName.ToLower()))
                    defaultFilesInfo.FileInfo[fileInfo.FileName.ToLower()] = fileInfo;
                else
                    defaultFilesInfo.FileInfo.Add(fileInfo.FileName.ToLower(), fileInfo);

            //
            var newFilesInfo2 = await GenerateOverrideFilesInfo(defaultFilesInfo, inputFolderB,
                $"{outputFolder}bin_override_b", baseHttpLink + "bin_override_b/", progress, ct);

            newFilesInfo2.ToXml($"{outputFolder}manifest_override_b.xml");
        }

        private static async Task<FilesInfo> GenerateOverrideFilesInfo(FilesInfo defaultFilesInfo, string inputFolder,
            string outputFolder, string baseHttpLink, IProgress<ZipFileProgress> progress,
            CancellationToken ct)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            //
            if (!outputFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                outputFolder += Path.DirectorySeparatorChar;

            var newFilesInfo = new FilesInfo();
            foreach (var file in Directory.GetFiles(inputFolder, "*", SearchOption.AllDirectories))
            {
                //
                var rootPath = inputFolder;
                if (!rootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    rootPath += Path.DirectorySeparatorChar;

                var fileName = file.Replace(rootPath, string.Empty);

                //Ignore unchanged file
                if (defaultFilesInfo.FileInfo.ContainsKey(fileName.ToLower()))
                {
                    var crc32 = Crc32Utils.GetCrc32File(file);
                    if (defaultFilesInfo.FileInfo[fileName.ToLower()].Crc32 == crc32)
                        continue;
                }

                //
                var binFileName = $"{Crc32Utils.GetCrc32FromString(fileName.ToLower()):X4}.bin";

                //
                var outFileName = $"{outputFolder}{binFileName}";
                await ZipUtils.DoCreateL33TZipFile(file, outFileName, progress, ct);

                //
                var fileInfo = new FileInfo
                {
                    FileName = fileName,
                    Crc32 = Crc32Utils.GetCrc32File(file),
                    Size = new System.IO.FileInfo(file).Length,
                    HttpLink = $"{baseHttpLink}{binFileName}",
                    BinCrc32 = Crc32Utils.GetCrc32File(outFileName),
                    BinSize = new System.IO.FileInfo(outFileName).Length
                };

                //
                newFilesInfo.FileInfo.Add(fileInfo.FileName.ToLower(), fileInfo);
            }

            return newFilesInfo;
        }
    }
}