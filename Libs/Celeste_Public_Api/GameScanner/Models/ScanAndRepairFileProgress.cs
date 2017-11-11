#region Using directives

using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Public_Api.GameScanner.Models
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
            ZipFileProgress extractProgress)
        {
            FileName = fileName;
            TotalProgressPercentage = totalProgressPercentage;
            L33TZipExtractProgress = extractProgress;
        }

        public string FileName { get; }

        public int TotalProgressPercentage { get; }

        public ExLog ProgressLog { get; }

        public DownloadFileProgress DownloadFileProgress { get; }

        public ZipFileProgress L33TZipExtractProgress { get; }
    }
}