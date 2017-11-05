#region Using directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml.Serialization;
using Celeste_Public_Api.GameFileInfo.Progress;
using Celeste_Public_Api.Logger;
using Crc32;
using Ionic.Zlib;

#endregion

namespace Celeste_Public_Api.GameFileInfo
{
    public enum DownloadState
    {
        Unknow = 0,
        Idle = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        Failed = 5
    }

    [XmlRoot(ElementName = "GameFile")]
    public class GameFile
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        [XmlAttribute(AttributeName = "FileName")]
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "CRC32")]
        public uint Crc32 { get; set; }

        [XmlAttribute(AttributeName = "Size")]
        public ulong Size { get; set; }

        [XmlAttribute(AttributeName = "HttpLink")]
        public string HttpLink { get; set; }

        [XmlAttribute(AttributeName = "BinCRC32")]
        public uint BinCrc32 { get; set; }

        [XmlAttribute(AttributeName = "BinSize")]
        public ulong BinSize { get; set; }

        [XmlIgnore]
        public IProgress<ExProgressGameFile> Progress { get;} = new Progress<ExProgressGameFile>();

        public void Scan(string gameFilePath, EventHandler<ExProgressGameFile> eventHandler, CancellationTokenSource cts)
        {
            ScanFile(gameFilePath, true, eventHandler, cts);
        }

        public void ScanAndRepair(string gameFilePath, EventHandler<ExProgressGameFile> eventHandler, CancellationTokenSource cts)
        {
            ScanFile(gameFilePath, false, eventHandler, cts);
        }

        private void ScanFile(string gameFilePath, bool ignoreDownload, EventHandler<ExProgressGameFile> eventHandler, CancellationTokenSource cts)
        {
            var ct = cts.Token;
            var filePath = $"{gameFilePath}{FileName}";
            try
            {
                ((Progress<ExProgressGameFile>)Progress).ProgressChanged += eventHandler;
                //INIT
                Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.Init,
                    new ExLog(LogLevel.Info, $"[{FileName}]")));

                //File Check
                if (File.Exists(filePath))
                {
                    Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CheckFile,
                        new ExLog(LogLevel.Debug, $"File '{gameFilePath}{FileName}' found.")));

                    //CRC Check
                    Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CheckFileCrc,
                        new ExLog(LogLevel.Info, "Check file crc32...")));

                    //CRC Check Result
                    if (Crc32Check(filePath, Crc32))
                    {
                        Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.End,
                            new ExLog(LogLevel.Debug, "File crc32 is valid.")));

                        //Exit
                        return;
                    }

                    Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CheckFileCrcDone,
                        new ExLog(LogLevel.Warn, "File crc32 is invalid.")));
                }
                else
                {
                    Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CheckFile,
                        new ExLog(LogLevel.Warn, $"File '{gameFilePath}{FileName}' not found.")));
                }

                if (ignoreDownload)
                {
                    Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.End,
                        new ExLog(LogLevel.Debug, "File download ignored.")));

                    //Exit
                    goto end;
                }

                //Download File
                Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.DownloadFile,
                    new ExLog(LogLevel.Info, "File downloading...")));

                var tempFileName = Path.GetTempFileName();

                if (StartDownloadFile(tempFileName, ct))
                {
                    while (DownloadState == DownloadState.InProgress)
                    {
                        //Just Wait
                    }

                    //Download File Completed

                    if (DownloadState == DownloadState.Completed)
                    {
                        Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.DownloadFileDone,
                            new ExLog(LogLevel.Debug, "Download completed.")));

                        //Download File crc check
                        Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CheckDownloadFileCrc,
                            new ExLog(LogLevel.Info, "Check downloaded file crc32...")));

                        //Download File crc check result
                        if (Crc32Check(tempFileName, BinCrc32))
                        {
                            Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CheckDownloadFileCrc,
                                new ExLog(LogLevel.Debug, "Downloaded file is valid.")));
                        }
                        else
                        {
                            if (File.Exists(tempFileName))
                                File.Delete(tempFileName);

                            throw new Exception("Downloaded file is invalid!");
                        }

                        //IsL33TFile
                        var tmpFilePath = tempFileName;
                        var tempFileName2 = Path.GetTempFileName();
                        if (IsL33TFile(tempFileName))
                        {
                            Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.ExtractDownloadFile,
                                new ExLog(LogLevel.Info, "Extract downloaded file...")));

                            //Decompress File
                            GetDecompressedFile(tempFileName, tempFileName2);
                            tmpFilePath = tempFileName2;
                            Progress.Report(new ExProgressGameFile(FileName,
                                StepProgressGameFile.ExtractDownloadFileDone,
                                new ExLog(LogLevel.Debug, "File decompressed.")));

                            //Download File crc check
                            Progress.Report(new ExProgressGameFile(FileName,
                                StepProgressGameFile.CheckExtractDownloadFileCrc,
                                new ExLog(LogLevel.Info, "Check extracted downloaded file crc32...")));

                            //Uncompressed download File crc check result
                            if (Crc32Check(tmpFilePath, Crc32))
                            {
                                Progress.Report(new ExProgressGameFile(FileName,
                                    StepProgressGameFile.CheckExtractDownloadFileCrcDone,
                                    new ExLog(LogLevel.Debug, "Extracted downloaded file is valid.")));
                            }
                            else
                            {
                                if (File.Exists(tempFileName))
                                    File.Delete(tempFileName);

                                if (File.Exists(tempFileName2))
                                    File.Delete(tempFileName2);

                                throw new Exception("Extracted downloaded file is invalid!");
                            }
                        }
                        else
                        {
                            Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.ExtractDownloadFile,
                                new ExLog(LogLevel.Debug, "File is not compressed.")));
                        }

                        //Copy File to game folder
                        Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CopyNewFile,
                            new ExLog(LogLevel.Info, "Copying new file...")));

                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        var pathName = Path.GetDirectoryName(filePath);
                        if (pathName != null && !Directory.Exists(pathName))
                            Directory.CreateDirectory(pathName);

                        File.Move(tmpFilePath, filePath);

                        //Removing temporary file
                        Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.CleanUpTempFile,
                            new ExLog(LogLevel.Info, "Clean-up temporary file...")));

                        if (File.Exists(tempFileName))
                            File.Delete(tempFileName);

                        if (File.Exists(tempFileName2))
                            File.Delete(tempFileName2);
                    }
                    else
                    {
                        if (File.Exists(tempFileName))
                            File.Delete(tempFileName);

                        throw new Exception($"Download file failed ({DownloadState})!");
                    }
                }
                else
                {
                    if (File.Exists(tempFileName))
                        File.Delete(tempFileName);

                    throw new Exception("Start download failed!");
                }
                end:

                //#100
                Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.End,
                    new ExLog(LogLevel.Debug, "End.")));
            }
            catch (Exception e)
            {
                Progress.Report(new ExProgressGameFile(FileName, StepProgressGameFile.End,
                    new ExLog(LogLevel.Error, $"Exception: {e.Message}")));
            }
            finally
            {
                ((Progress<ExProgressGameFile>)Progress).ProgressChanged -= eventHandler;
            }
        }


        #region Crc32Check

        private static bool Crc32Check(string filePath, uint crc32)
        {
            bool retVal;
            try
            {
                var crc32Algo = new Crc32Algorithm();
                using (var fs = File.Open(filePath, FileMode.Open))
                {
                    var result = crc32Algo.ComputeHash(fs);
                    Array.Reverse(result);

                    var realCrc32 = BitConverter.ToUInt32(result, 0);

                    retVal = realCrc32 == crc32;
                }
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }

        #endregion

        #region Download

        [XmlIgnore]
        private DownloadState DownloadState { get; set; } = DownloadState.Idle;

        private bool StartDownloadFile(string tempFileName, CancellationToken ct)
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFileCompleted += Completed;
                    webClient.DownloadProgressChanged += ProgressChanged;
                    _stopwatch.Start();
                    DownloadState = DownloadState.InProgress;
                    using (var registration = ct.Register(() => webClient.CancelAsync()))
                    {
                        webClient.DownloadFileAsync(new Uri(HttpLink), tempFileName, ct);
                    }
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
                {
                    // ignore this exception
                }
                catch (Exception)
                {
                    DownloadState = DownloadState.Failed;
                    throw;
                }
                return true;
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if(((CancellationToken) e.UserState).IsCancellationRequested)
            {
                ((WebClient)sender).CancelAsync();
            }

            Progress.Report(new ExProgressGameFile(FileName,
                new ExDownloadProgress(_stopwatch.Elapsed.TotalMilliseconds, e.ProgressPercentage, e.BytesReceived,
                    e.TotalBytesToReceive)));
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            _stopwatch.Stop();
            if (e.Error != null)
            {
                DownloadState = DownloadState.Failed;
                return;
            }
            DownloadState = e.Cancelled ? DownloadState.Cancelled : DownloadState.Completed;
        }

        #endregion

        #region L33TFile

        private static bool IsL33TFile(string fileName)
        {
            bool result;
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    reader.BaseStream.Position = 0L;
                    var head = new string(reader.ReadChars(4));
                    result = head == "l33t";
                }
            }
            return result;
        }

        private static void GetDecompressedFile(string fileName, string outputFileName)
        {
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    reader.BaseStream.Position = 0L;
                    //
                    var head = new string(reader.ReadChars(4));
                    if (head.ToLower() != "l33t")
                        throw new FileLoadException($"'l33t' header not found, file: '{fileName}'");
                    var length = reader.ReadInt32();
                    //Skip deflate specification (2 Byte)
                    reader.BaseStream.Position = 10L;
                    using (var a = new DeflateStream(reader.BaseStream, CompressionMode.Decompress))
                    {
                        using (var fileStreamFinal = File.Open(outputFileName, FileMode.Create))
                        {
                            using (var final = new BinaryWriter(fileStreamFinal))
                            {
                                var buffer = new byte[8192];
                                int read;
                                var totalread = 0;
                                while ((read = a.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    if (read > length)
                                    {
                                        totalread += length;
                                        final.Write(buffer, 0, length);
                                    }
                                    else if (totalread + read <= length)
                                    {
                                        totalread += read;
                                        final.Write(buffer, 0, read);
                                    }
                                    else if (totalread + read > length)
                                    {
                                        totalread += length - totalread;
                                        final.Write(buffer, 0, length - totalread);
                                    }

                                    if (totalread == length)
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}