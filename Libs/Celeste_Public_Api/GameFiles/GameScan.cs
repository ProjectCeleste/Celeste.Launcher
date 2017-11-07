#region Using directives

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Celeste_Public_Api.GameFiles.Def;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Public_Api.GameFiles
{
    public class ScanAndRepairFileProgress
    {
        public ScanAndRepairFileProgress(string fileName, int totalProgressPercentage)
        {
            FileName = fileName;
            TotalProgressPercentage = totalProgressPercentage;
        }

        public ScanAndRepairFileProgress(string fileName, int totalProgressPercentage, ExLog progressLog)
        {
            FileName = fileName;
            TotalProgressPercentage = totalProgressPercentage;
            ProgressLog = progressLog;
        }

        public ScanAndRepairFileProgress(string fileName, int totalProgressPercentage,
            DownloadFileProgress downloadProgress)
        {
            FileName = fileName;
            TotalProgressPercentage = totalProgressPercentage;
            DownloadFileProgress = downloadProgress;
        }

        public ScanAndRepairFileProgress(string fileName, int totalProgressPercentage,
            L33TZipExtractProgress extractProgress)
        {
            FileName = fileName;
            TotalProgressPercentage = totalProgressPercentage;
            L33TZipExtractProgress = extractProgress;
        }

        public string FileName { get; }

        public int TotalProgressPercentage { get; }

        public ExLog ProgressLog { get; }

        public DownloadFileProgress DownloadFileProgress { get; }

        public L33TZipExtractProgress L33TZipExtractProgress { get; }
    }

    public class GameScan
    {
        public static async Task<bool> ScanAndRepairFile(GameFile fileInfo, string gameFilePath,
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

                if (Files.FileCheck(filePath, fileInfo.Size, fileInfo.Crc32))
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
                var x = new DownloadFile(new Uri(fileInfo.HttpLink), tempFileName, dowloadProgress);
                await x.DownloadFileAsync(ct);

                //#65 Check Downloaded File
                progress.Report(new ScanAndRepairFileProgress(fileInfo.FileName, 65,
                    new ExLog(LogLevel.Info, "      - Checking downloaded file...")));

                if (!Files.FileCheck(tempFileName, fileInfo.BinSize, fileInfo.BinCrc32))
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
                    if (!Files.FileCheck(tempFileName2, fileInfo.Size, fileInfo.Crc32))
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
    }
}