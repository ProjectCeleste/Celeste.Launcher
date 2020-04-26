using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectCeleste.GameFiles.GameScanner.FileDownloader;
using ProjectCeleste.GameFiles.GameScanner.Models;
using ProjectCeleste.GameFiles.Tools.L33TZip;
using ProjectCeleste.GameFiles.Tools.Misc;

namespace ProjectCeleste.GameFiles.GameScanner
{
    public sealed class GameScannerManager : IDisposable
    {
        private static readonly string GameScannerTempPath =
            Path.Combine(Path.GetTempPath(), "ProjectCeleste.GameFiles.GameScanner", "Temp");

        private readonly string _filesRootPath;

        private readonly bool _isSteam;

        private bool _scanIsRunning;

        private CancellationTokenSource _cts;

        private IEnumerable<GameFileInfo> _gameFiles;

        public GameScannerManager(bool isSteam = false) : this(GetGameFilesRootPath(), isSteam)
        {
        }

        public GameScannerManager(string filesRootPath, bool isSteam = false)
        {
            if (string.IsNullOrEmpty(filesRootPath))
                throw new ArgumentException("Game files path is null or empty", nameof(filesRootPath));

            _filesRootPath = filesRootPath;
            _isSteam = isSteam;
            _scanIsRunning = false;
        }

        public void Dispose()
        {
            Abort();

            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }

            CleanUpTmpFolder();
        }

        public async Task InitializeFromCelesteManifest()
        {
            if (_gameFiles?.Any() == true)
                throw new Exception("Already Initialized");

            CleanUpTmpFolder();

            var gameFileInfos =
                (await GameFilesInfoFromCelesteManifest(_isSteam)).GameFileInfo.Select(key => key.Value);
            var fileInfos = gameFileInfos as GameFileInfo[] ?? gameFileInfos.ToArray();
            if (fileInfos.Length == 0)
                throw new ArgumentException("Game files info is null or empty", nameof(gameFileInfos));

            _gameFiles = fileInfos;
        }

        public async Task InitializeFromGameManifest(string type = "production",
            int build = 6148)
        {
            if (_gameFiles?.Any() == true)
                throw new Exception("Already Initialized");

            CleanUpTmpFolder();

            var gameFileInfos =
                (await GameFilesInfoFromGameManifest(type, build, _isSteam)).GameFileInfo.Select(key => key.Value);
            var fileInfos = gameFileInfos as GameFileInfo[] ?? gameFileInfos.ToArray();
            if (fileInfos.Length == 0)
                throw new ArgumentException("Game files info is null or empty", nameof(gameFileInfos));

            _gameFiles = fileInfos;
        }

        public async Task<bool> Scan(bool quick = true, IProgress<ScanProgress> progress = null)
        {
            EnsureInitialized();
            EnsureGameScannerIsNotRunning();

            _scanIsRunning = true;

            var retVal = true;
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();
                _cts = new CancellationTokenSource();
                var token = _cts.Token;

                var totalSize = _gameFiles.Select(key => key.Size).Sum();
                var currentSize = 0L;
                var index = 0;
                var totalIndex = _gameFiles.Count();
                progress?.Report(new ScanProgress(string.Empty, 0, 0, totalIndex));
                if (quick)
                {
                    Parallel.ForEach(_gameFiles, (fileInfo, state) =>
                    {
                        try
                        {
                            token.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException)
                        {
                            state.Break();
                            throw;
                        }

                        if (!RunFileQuickCheck(Path.Combine(_filesRootPath, fileInfo.FileName), fileInfo.Size))
                        {
                            retVal = false;
                            state.Break();
                        }

                        var currentIndex = Interlocked.Increment(ref index);
                        double newSize = Interlocked.Add(ref currentSize, fileInfo.Size);

                        progress?.Report(new ScanProgress(fileInfo.FileName, newSize / totalSize * 100,
                            currentIndex, totalIndex));
                    });
                }
                else
                {
                    var fileInfos = _gameFiles.ToArray();
                    for (var i = 0; i < fileInfos.Length; i++)
                    {
                        var fileInfo = fileInfos[i];
                        token.ThrowIfCancellationRequested();

                        if (!await RunFileCheck(Path.Combine(_filesRootPath, fileInfo.FileName), fileInfo.Size,
                            fileInfo.Crc32, token))
                            return false;

                        currentSize += fileInfo.Size;

                        progress?.Report(new ScanProgress(fileInfo.FileName, currentSize / totalSize * 100,
                            i + 1, totalIndex));
                    }
                }
            }
            finally
            {
                _scanIsRunning = false;
            }

            return retVal;
        }

        public async Task<bool> ScanAndRepair(IProgress<ScanProgress> progress = null,
            IProgress<ScanSubProgress> subProgress = null, int concurrentDownload = 0)
        {
            EnsureInitialized();
            EnsureGameScannerIsNotRunning();

            _scanIsRunning = true;

            try
            {
                if (_cts != null)
                {
                    _cts.Cancel();
                    _cts.Dispose();
                }

                _cts = new CancellationTokenSource();

                var token = _cts.Token;

                CleanUpTmpFolder();

                var retVal = false;

                var totalSize = _gameFiles.Select(key => key.BinSize).Sum();
                var globalProgress = 0L;
                var totalIndex = _gameFiles.Count();
                var gameFiles = _gameFiles.OrderByDescending(key => key.FileName.Contains("\\"))
                    .ThenBy(key => key.FileName).ToArray();
                for (var i = 0; i < gameFiles.Length; i++)
                {
                    token.ThrowIfCancellationRequested();

                    var fileInfo = gameFiles[i];

                    progress?.Report(new ScanProgress(fileInfo.FileName,
                        (double) globalProgress / totalSize * 100, i, totalIndex));

                    retVal = await ScanAndRepairFile(fileInfo, _filesRootPath, subProgress, concurrentDownload, token);

                    if (!retVal)
                        break;

                    globalProgress += fileInfo.BinSize;
                }

                return retVal;
            }
            finally
            {
                await Task.Factory.StartNew(CleanUpTmpFolder);

                _scanIsRunning = false;
            }
        }

        public void Abort()
        {
            if (!_scanIsRunning)
                return;

            if (_cts?.IsCancellationRequested == false)
                _cts.Cancel();
        }

        private void EnsureInitialized()
        {
            if (_gameFiles?.Any() != true)
                throw new Exception("Game scanner has not been initialized or no game files was found");
        }

        private void EnsureGameScannerIsNotRunning()
        {
            if (_scanIsRunning)
                throw new Exception("Scan is already running");
        }

        private static void CleanUpTmpFolder()
        {
            if (!Directory.Exists(GameScannerTempPath))
                return;

            try
            {
                var files = new DirectoryInfo(GameScannerTempPath).GetFiles("*", SearchOption.AllDirectories);

                if (files.Length > 0)
                    Parallel.ForEach(files, file =>
                    {
                        try
                        {
                            File.Delete(file.FullName);
                        }
                        catch (Exception)
                        {
                            //
                        }
                    });

                Directory.Delete(GameScannerTempPath, true);
            }
            catch (Exception)
            {
                //
            }
        }

        #region GameFile

        public static async Task EnsureValidGameFile(string gameFilePath, long expectedFileSize, uint expectedCrc32,
            CancellationToken ct = default, IProgress<double> progress = null)
        {
            var gameFileInfo = new FileInfo(gameFilePath);

            if (!gameFileInfo.Exists)
                throw new Exception($"The game file {gameFilePath} does not exist");

            if (gameFileInfo.Length != expectedFileSize)
                throw new Exception(
                    $"The game file {gameFilePath} was expected to have a size of {expectedFileSize} but was {gameFileInfo.Length}");

            var gameFileCrc32 = await Crc32Utils.DoGetCrc32FromFile(gameFilePath, ct, progress);

            if (gameFileCrc32 != expectedCrc32)
                throw new Exception(
                    $"The game file {gameFilePath} was expected to have a crc32 {expectedCrc32} but was {gameFileCrc32}");
        }

        public static async Task<bool> RunFileCheck(string gameFilePath, long expectedFileSize, uint expectedCrc32,
            CancellationToken ct = default, IProgress<double> progress = null)
        {
            return RunFileQuickCheck(gameFilePath, expectedFileSize) &&
                   expectedCrc32 == await Crc32Utils.DoGetCrc32FromFile(gameFilePath, ct, progress);
        }

        public static bool RunFileQuickCheck(string gameFilePath, long expectedFileSize)
        {
            var fi = new FileInfo(gameFilePath);
            return fi.Exists && fi.Length == expectedFileSize;
        }

        public static async Task<bool> ScanAndRepairFile(GameFileInfo fileInfo, string gameFilePath,
            IProgress<ScanSubProgress> progress = null, int concurrentDownload = 0,  CancellationToken ct = default)
        {
            var filePath = Path.Combine(gameFilePath, fileInfo.FileName);

            //#1 Check File
            ct.ThrowIfCancellationRequested();
            progress?.Report(new ScanSubProgress(ScanSubProgressStep.Check, 0));

            Progress<double> subProgressCheck = null;
            if (progress != null)
            {
                subProgressCheck = new Progress<double>();
                subProgressCheck.ProgressChanged += (o, d) =>
                {
                    progress.Report(new ScanSubProgress(
                        ScanSubProgressStep.Check, d));
                };
            }

            if (await RunFileCheck(filePath, fileInfo.Size, fileInfo.Crc32, ct, subProgressCheck))
            {
                progress?.Report(new ScanSubProgress(ScanSubProgressStep.End, 100));
                return true;
            }

            //#2 Download File
            ct.ThrowIfCancellationRequested();
            progress?.Report(new ScanSubProgress(ScanSubProgressStep.Download, 0));

            var tempFileName = Path.Combine(GameScannerTempPath, $"{fileInfo.FileName.GetHashCode():X4}.tmp");
            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            var fileDownloader = concurrentDownload == 1
                ? (IFileDownloader) new SimpleFileDownloader(fileInfo.HttpLink, tempFileName)
                : new ChunkFileDownloader(fileInfo.HttpLink, tempFileName, GameScannerTempPath,
                    concurrentDownload);

            if (progress != null)
                fileDownloader.ProgressChanged += (sender, eventArg) =>
                {
                    switch (fileDownloader.State)
                    {
                        case FileDownloaderState.Invalid:
                        case FileDownloaderState.Download:
                            progress.Report(new ScanSubProgress(
                                ScanSubProgressStep.Download, fileDownloader.DownloadProgress * 0.99,
                                new ScanDownloadProgress(fileDownloader.DownloadSize, fileDownloader.BytesDownloaded,
                                    fileDownloader.DownloadSpeed)));
                            break;
                        case FileDownloaderState.Finalize:
                            progress.Report(new ScanSubProgress(
                                ScanSubProgressStep.Download, 99,
                                new ScanDownloadProgress(fileDownloader.DownloadSize, fileDownloader.BytesDownloaded,
                                    0)));
                            break;
                        case FileDownloaderState.Complete:
                            progress.Report(new ScanSubProgress(
                                ScanSubProgressStep.Download, 100,
                                new ScanDownloadProgress(fileDownloader.DownloadSize, fileDownloader.BytesDownloaded,
                                    0)));
                            break;
                        case FileDownloaderState.Error:
                        case FileDownloaderState.Abort:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(fileDownloader.State),
                                fileDownloader.State, null);
                    }
                };

            await fileDownloader.DownloadAsync(ct);


            //#3 Check Downloaded File
            ct.ThrowIfCancellationRequested();

            Progress<double> subProgressCheckDown = null;
            if (progress != null)
            {
                progress.Report(new ScanSubProgress(ScanSubProgressStep.CheckDownload, 0));

                subProgressCheckDown = new Progress<double>();
                subProgressCheckDown.ProgressChanged += (o, d) =>
                {
                    progress.Report(new ScanSubProgress(
                        ScanSubProgressStep.CheckDownload, d));
                };
            }

            try
            {
                await EnsureValidGameFile(tempFileName, fileInfo.BinSize, fileInfo.BinCrc32, ct, subProgressCheckDown);
            }
            catch
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);

                throw;
            }

            //#4 Extract downloaded file
            ct.ThrowIfCancellationRequested();
            if (L33TZipUtils.IsL33TZipFile(tempFileName))
            {
                var tempFileName2 = $"{tempFileName.Replace(".tmp", string.Empty)}.ext.tmp";
                //
                Progress<double> extractProgress = null;
                if (progress != null)
                {
                    progress.Report(new ScanSubProgress(
                        ScanSubProgressStep.ExtractDownload, 0));

                    extractProgress = new Progress<double>();
                    extractProgress.ProgressChanged += (o, d) =>
                    {
                        progress.Report(new ScanSubProgress(
                            ScanSubProgressStep.ExtractDownload, d));
                    };
                }

                await L33TZipUtils.ExtractL33TZipFileAsync(tempFileName, tempFileName2, ct, extractProgress);

                //#4.1 Check Extracted File
                ct.ThrowIfCancellationRequested();
                Progress<double> subProgressCheckExt = null;
                if (progress != null)
                {
                    progress.Report(new ScanSubProgress(
                        ScanSubProgressStep.CheckExtractDownload, 0));

                    subProgressCheckExt = new Progress<double>();
                    subProgressCheckExt.ProgressChanged += (o, d) =>
                    {
                        progress.Report(new ScanSubProgress(
                            ScanSubProgressStep.CheckExtractDownload, d));
                    };
                }

                await EnsureValidGameFile(tempFileName2, fileInfo.Size, fileInfo.Crc32, ct, subProgressCheckExt);

                File.Delete(tempFileName);

                tempFileName = tempFileName2;
            }

            //#5 Move new file to game folder
            ct.ThrowIfCancellationRequested();

            progress?.Report(new ScanSubProgress(
                ScanSubProgressStep.Finalize, 0));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                var pathName = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(pathName) && !Directory.Exists(pathName))
                    Directory.CreateDirectory(pathName);
            }

            File.Move(tempFileName, filePath);

            //#6 End
            progress?.Report(new ScanSubProgress(
                ScanSubProgressStep.End, 100));

            return true;
        }

        #endregion

        #region Get GameFilesInfo

        public static string GetGameFilesRootPath()
        {
            {
                //Custom Path 1
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Spartan.exe");
                if (File.Exists(path))
                    return Path.GetDirectoryName(path);

                //Custom Path 2
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AOEO", "Spartan.exe");
                if (File.Exists(path))
                    return Path.GetDirectoryName(path);

                //Custom Path 3
                if (Environment.Is64BitOperatingSystem)
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                        "Age Of Empires Online", "Spartan.exe");
                    if (File.Exists(path))
                        return Path.GetDirectoryName(path);
                }

                //Custom Path 4
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    "Age Of Empires Online", "Spartan.exe");
                if (File.Exists(path))
                    return Path.GetDirectoryName(path);

                //Steam 1
                if (Environment.Is64BitOperatingSystem)
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam",
                        "steamapps", "common", "Age Of Empires Online", "Spartan.exe");
                    if (File.Exists(path))
                        return Path.GetDirectoryName(path);
                }

                //Steam 2
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam",
                    "steamapps", "common", "Age Of Empires Online", "Spartan.exe");
                if (File.Exists(path))
                    return Path.GetDirectoryName(path);

                //Original Game Path
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Local",
                    "Microsoft", "Age Of Empires Online", "Spartan.exe");
                return File.Exists(path)
                    ? Path.GetDirectoryName(path)
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AOEO");
            }
        }

        public static async Task<GameFilesInfo> GameFilesInfoFromGameManifest(string type = "production",
            int build = 6148, bool isSteam = false)
        {
            string txt;
            using (var client = new WebClient())
            {
                txt = await client.DownloadStringTaskAsync(
                    $"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/manifest.txt");
            }

            var retVal = from line in txt.Split(new[] {Environment.NewLine, "\r\n"},
                    StringSplitOptions.RemoveEmptyEntries)
                where line.StartsWith("+")
                where
                    // Launcher
                    !line.StartsWith("+AoeOnlineDlg.dll", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+AoeOnlinePatch.dll", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+expapply.dll", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherLocList.txt", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherStrings-de-DE.xml", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherStrings-en-US.xml", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherStrings-es-ES.xml", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherStrings-fr-FR.xml", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherStrings-it-IT.xml", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherStrings-zh-CHT.xml", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+AOEOnline.exe.cfg", StringComparison.OrdinalIgnoreCase) &&
                    //Beta Launcher
                    !line.StartsWith("+Launcher.exe", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherReplace.exe", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+LauncherLocList.txt", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+AOEO_Privacy.rtf", StringComparison.OrdinalIgnoreCase) &&
                    !line.StartsWith("+pw32b.dll", StringComparison.OrdinalIgnoreCase) &&
                    //Steam                      
                    (!line.StartsWith("+steam_api.dll", StringComparison.OrdinalIgnoreCase) || isSteam &&
                     line.StartsWith("+steam_api.dll", StringComparison.OrdinalIgnoreCase)) &&
                    //Junk
                    !line.StartsWith("+t3656t4234.tmp", StringComparison.OrdinalIgnoreCase)
                select line.Split('|')
                into lineSplit
                select new GameFileInfo(lineSplit[0].Substring(1, lineSplit[0].Length - 1),
                    Convert.ToUInt32(lineSplit[1]),
                    Convert.ToInt64(lineSplit[2]),
                    $"http://spartan.msgamestudios.com/content/spartan/{type}/{build}/{lineSplit[3]}",
                    Convert.ToUInt32(lineSplit[4]),
                    Convert.ToInt64(lineSplit[5]));

            return new GameFilesInfo(new Version(4, 0, 0, 6148), retVal);
        }

        public static async Task<GameFilesInfo> GameFilesInfoFromCelesteManifest(bool isSteam = false)
        {
            //Load default manifest
            var gameFilesInfo = await GameFilesInfoFromGameManifest(isSteam: isSteam);

            //Load Celeste override
            string manifestJsonContents;
            using (var client = new WebClient())
            {
                manifestJsonContents = await client.DownloadStringTaskAsync(
                    "https://downloads.projectceleste.com/game_files/manifest_override.json");
            }

            var gameFilesInfoOverride = JsonConvert.DeserializeObject<GameFilesInfo>(manifestJsonContents);
            gameFilesInfo.Version = gameFilesInfoOverride.Version;
            foreach (var fileInfo in gameFilesInfoOverride.GameFileInfo.Select(key => key.Value))
                gameFilesInfo.GameFileInfo[fileInfo.FileName] = fileInfo;

            //Load xLive override
            string manifestXLiveJsonContents;
            using (var client = new WebClient())
            {
                manifestXLiveJsonContents = await client.DownloadStringTaskAsync(
                    "https://downloads.projectceleste.com/game_files/xlive.json");
            }

            gameFilesInfo.GameFileInfo["xlive.dll"] =
                JsonConvert.DeserializeObject<GameFileInfo>(manifestXLiveJsonContents);

            //
            return gameFilesInfo;
        }

        #endregion

        #region Create GameFilesInfo Package

        public static async Task CreateGameUpdatePackage(string inputFolder, string outputFolder,
            string baseHttpLink, Version buildId, CancellationToken ct = default)
        {
            var finalOutputFolder = Path.Combine(outputFolder, "bin_override", buildId.ToString());

            if (!baseHttpLink.EndsWith("/"))
                baseHttpLink += "/";
            baseHttpLink = Path.Combine(baseHttpLink, "bin_override", buildId.ToString()).Replace("\\", "/");

            var gameFiles = await GenerateGameFilesInfo(inputFolder, finalOutputFolder, baseHttpLink, buildId, ct);

            if (gameFiles.GameFileInfo.Count < 1)
                throw new Exception($"No game files found in {inputFolder}");

            var manifestJsonContents = JsonConvert.SerializeObject(gameFiles, Formatting.Indented);
            File.WriteAllText(Path.Combine(finalOutputFolder, $"manifest_override-{buildId}.json"),
                manifestJsonContents,
                Encoding.UTF8);
            File.WriteAllText(Path.Combine(outputFolder, "manifest_override.json"), manifestJsonContents,
                Encoding.UTF8);
        }

        public static async Task CreateXLiveUpdatePackage(string xLivePath, string outputFolder,
            string baseHttpLink, Version buildId, CancellationToken ct = default)
        {
            var finalOutputFolder = Path.Combine(outputFolder, "xlive", buildId.ToString());

            if (!baseHttpLink.EndsWith("/"))
                baseHttpLink += "/";
            baseHttpLink = Path.Combine(baseHttpLink, "xlive", buildId.ToString()).Replace("\\", "/");

            var xLiveInfo = await GenerateGameFileInfo(xLivePath, "xlive.dll", finalOutputFolder, baseHttpLink, ct);

            var manifestJsonContents = JsonConvert.SerializeObject(xLiveInfo, Formatting.Indented);
            File.WriteAllText(Path.Combine(finalOutputFolder, $"xlive-{buildId}.json"), manifestJsonContents,
                Encoding.UTF8);
            File.WriteAllText(Path.Combine(outputFolder, "xlive.json"), manifestJsonContents, Encoding.UTF8);
        }

        public static async Task<GameFilesInfo> GenerateGameFilesInfo(string inputFolder, string outputFolder,
            string baseHttpLink, Version buildId, CancellationToken ct = default)
        {
            if (Directory.Exists(outputFolder))
                Directory.Delete(outputFolder, true);

            Directory.CreateDirectory(outputFolder);

            var newFilesInfo = new List<GameFileInfo>();
            foreach (var file in Directory.GetFiles(inputFolder, "*", SearchOption.AllDirectories))
            {
                ct.ThrowIfCancellationRequested();

                var rootPath = inputFolder;
                if (!rootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    rootPath += Path.DirectorySeparatorChar;

                var fileName = file.Replace(rootPath, string.Empty);

                var newInfo = await GenerateGameFileInfo(file, fileName, outputFolder, baseHttpLink, ct);

                newFilesInfo.Add(newInfo);
            }

            return new GameFilesInfo(buildId, newFilesInfo);
        }

        public static async Task<GameFileInfo> GenerateGameFileInfo(string file, string fileName,
            string outputFolder, string baseHttpLink, CancellationToken ct = default)
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            if (!baseHttpLink.EndsWith("/"))
                baseHttpLink += "/";

            var binFileName = $"{fileName.ToLower().GetHashCode():X4}.bin";
            var outFileName = Path.Combine(outputFolder, binFileName);

            await L33TZipUtils.CompressFileAsL33TZipAsync(file, outFileName, ct);

            var fileCrc = await Crc32Utils.DoGetCrc32FromFile(file, ct);
            var fileLength = new FileInfo(file).Length;

            var externalLocation = Path.Combine(baseHttpLink, binFileName).Replace("\\", "/");
            var outFileCrc = await Crc32Utils.DoGetCrc32FromFile(outFileName, ct);
            var outFileLength = new FileInfo(outFileName).Length;

            return new GameFileInfo(fileName, fileCrc, fileLength, externalLocation,
                outFileCrc, outFileLength);
        }

        #endregion
    }
}