namespace ProjectCeleste.GameFiles.GameScanner.Models
{
    public enum ScanSubProgressStep : byte
    {
        Check,
        Download,
        CheckDownload,
        ExtractDownload,
        CheckExtractDownload,
        Finalize,
        End
    }

    public class ScanDownloadProgress
    {
        public ScanDownloadProgress(long size, long sizeCompleted, double speed)
        {
            Size = size;
            SizeCompleted = sizeCompleted;
            Speed = speed;
        }

        public long Size { get; }

        public long SizeCompleted { get; }

        public double Speed { get; }
    }

    public class ScanSubProgress
    {
        public ScanSubProgress(ScanSubProgressStep step,
            double progressPercentage, ScanDownloadProgress downloadProgressDownload = null)
        {
            Step = step;
            ProgressPercentage = progressPercentage;
            DownloadProgress = downloadProgressDownload;
        }

        public ScanSubProgressStep Step { get; }

        public double ProgressPercentage { get; }

        public ScanDownloadProgress DownloadProgress { get; }
    }
}