#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Celeste_Public_Api.GameFileInfo.Progress;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.GameFileInfo
{
    [XmlRoot(ElementName = "GameFiles")]
    public class GameFiles
    {
        public readonly object SyncLockCancel = new object();
        public readonly object SyncLockScan = new object();

        [XmlIgnore]
        private Dictionary<string, GameFile> GameFile { get; set; } = new Dictionary<string, GameFile>();

        [XmlElement(ElementName = "GameFile")]
        public GameFile[] GameFileArray
        {
            get
            {
                var list = new List<GameFile>();
                if (GameFile != null)
                    list.AddRange(GameFile.Keys.Select(key => GameFile[key]));

                return list.ToArray();
            }
            set
            {
                GameFile = new Dictionary<string, GameFile>();
                if (value == null) return;
                foreach (var item in value)
                    GameFile.Add(item.FileName, item);
            }
        }

        [XmlIgnore]
        public IProgress<ExProgressGameFiles> Progress { get; } = new Progress<ExProgressGameFiles>();

        [XmlIgnore]
        public bool IsScanRunning { get; private set; }

        [XmlIgnore]
        public int TotalCount { get; private set; }

        [XmlIgnore]
        public int CurrentIndex { get; private set; }

        [XmlIgnore]
        private CancellationTokenSource Cts { get; set; } = new CancellationTokenSource();

        public static GameFiles GetGameFiles()
        {
            var retVal = new GameFiles();

            //Load default manifest
            foreach (var fileInfo in FileInfoFromGameManifest("production", 6148))
                if (retVal.GameFile.ContainsKey(fileInfo.FileName))
                    retVal.GameFile[fileInfo.FileName] = fileInfo;
                else
                    retVal.GameFile.Add(fileInfo.FileName, fileInfo);

            //Override for celeste file
            //foreach (var fileInfo in FileInfoOverrideFromCelesteXml())
            //{
            //    if (retVal.GameFile.ContainsKey(fileInfo.FileName))
            //        retVal.GameFile[fileInfo.FileName] = fileInfo;
            //    else
            //        retVal.GameFile.Add(fileInfo.FileName, fileInfo);
            //}

            return retVal;
        }

        private static IEnumerable<GameFile> FileInfoFromGameManifest(string type, int build)
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
                select new GameFile
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

        private static IEnumerable<GameFile> FileInfoOverrideFromCelesteXml()
        {
            var tempFileName = Path.GetTempFileName();

            using (var client = new WebClient())
            {
                client.DownloadFile("https://projectceleste.com/static/celeste_gamefile_override.xml",
                    tempFileName);
            }

            // TODO

            var retVal = new List<GameFile>();

            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            return retVal;
        }

        public void ToXml(string filename)
        {
            XmlUtils.SerializeToFile(this, filename);
        }

        public void ProgressChanged(object sender, ExProgressGameFile e)
        {
            Progress.Report(new ExProgressGameFiles(TotalCount, CurrentIndex, e));
        }

        public void CancelScan()
        {
            if (!Monitor.TryEnter(SyncLockCancel))
                throw new Exception("CancelScan() already running!");

            try
            {
                if (IsScanRunning && !Cts.IsCancellationRequested)
                    Cts.Cancel();
            }
            finally
            {
                Monitor.Exit(SyncLockCancel);
            }
        }

        public async Task<bool> FullScanAndRepair(string gameFilePath, EventHandler<ExProgressGameFiles> eventHandler)
        {
            return await DoScanAndRepair(gameFilePath, eventHandler, false);
        }

        public async Task<bool> QuickScan(string gameFilePath, EventHandler<ExProgressGameFiles> eventHandler)
        {
            return await DoScanAndRepair(gameFilePath, eventHandler, true);
        }

        private async Task<bool> DoScanAndRepair(string gameFilePath, EventHandler<ExProgressGameFiles> eventHandler,
            bool isQuickScan)
        {
            if (!Monitor.TryEnter(SyncLockScan) || IsScanRunning)
                throw new Exception("Scan already running!");

            var retVal = false;
            IsScanRunning = true;
            try
            {
                if (string.IsNullOrEmpty(gameFilePath))
                    throw new ArgumentException("gameFilePath IsNullOrEmpty!");
                
                if (!Directory.Exists(gameFilePath))
                    Directory.CreateDirectory(gameFilePath);

                if (!gameFilePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    gameFilePath += Path.DirectorySeparatorChar;

                Cts.Cancel();
                Cts = new CancellationTokenSource();

                var t = Task.Run(() =>
                {
                    try
                    {
                        ((Progress<ExProgressGameFiles>) Progress).ProgressChanged += eventHandler;
                        var gameFileArray = GameFile.Values.ToArray();
                        TotalCount = gameFileArray.Length;
                        for (var index = 0; index < TotalCount; index++)
                        {
                            Cts.Token.ThrowIfCancellationRequested();
                            CurrentIndex = index;
                            var gameFile = gameFileArray[CurrentIndex];

                            if (isQuickScan)
                                gameFile.ScanAndRepair(gameFilePath, ProgressChanged, Cts.Token);
                            else
                                gameFile.ScanAndRepair(gameFilePath, ProgressChanged, Cts.Token);
                        }
                        retVal = true;
                    }
                    finally
                    {
                        ((Progress<ExProgressGameFiles>) Progress).ProgressChanged -= eventHandler;
                    }
                }, Cts.Token);

                await t;
            }
            finally
            {
                Monitor.Exit(SyncLockScan);
                IsScanRunning = false;
            }

            return retVal;
        }
    }
}