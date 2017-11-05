#region Using directives

using System;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Public_Api.GameFileInfo.Progress
{
    public enum StepProgressGameFile
    {
        Init = 0,
        CheckFile = 1,
        CheckFileCrc = 2,
        CheckFileCrcDone = 7,
        DownloadFile = 8,
        DownloadFileDone = 64,
        CheckDownloadFileCrc = 65,
        CheckDownloadFileCrcDone = 70,
        ExtractDownloadFile = 71,
        ExtractDownloadFileDone = 91,
        CheckExtractDownloadFileCrc = 92,
        CheckExtractDownloadFileCrcDone = 97,
        CopyNewFile = 98,
        CleanUpTempFile = 99,
        End = 100
    }

    public class ExProgressGameFile
    {
        public ExProgressGameFile(string fileName, StepProgressGameFile stepProgressGameFile)
        {
            FileName = fileName;
            StepProgressGameFile = stepProgressGameFile;
        }

        public ExProgressGameFile(string fileName, StepProgressGameFile stepProgressGameFile, ExLog progressLog)
        {
            FileName = fileName;
            StepProgressGameFile = stepProgressGameFile;
            ProgressLog = progressLog;
        }

        public ExProgressGameFile(string fileName, ExDownloadProgress downloadProgress)
        {
            FileName = fileName;
            StepProgressGameFile = StepProgressGameFile.DownloadFile;
            DownloadProgress = downloadProgress;
        }

        public ExProgressGameFile(string fileName, ExExtractProgress extractProgress)
        {
            FileName = fileName;
            StepProgressGameFile = StepProgressGameFile.ExtractDownloadFile;
            ExtractProgress = extractProgress;
        }

        public string FileName { get; }

        public int TotalProgressPercentage
        {
            get
            {
                switch (StepProgressGameFile)
                {
                    case StepProgressGameFile.Init:
                        return (int) StepProgressGameFile.Init;
                    case StepProgressGameFile.CheckFile:
                        return (int) StepProgressGameFile.CheckFile;
                    case StepProgressGameFile.CheckFileCrc:
                        return (int) StepProgressGameFile.CheckFileCrc;
                    case StepProgressGameFile.CheckFileCrcDone:
                        return (int) StepProgressGameFile.CheckFileCrcDone;
                    case StepProgressGameFile.DownloadFile:
                    {
                        if (DownloadProgress == null)
                            return (int) StepProgressGameFile.DownloadFile;

                        const int min = (int) StepProgressGameFile.DownloadFile;
                        const int max = (int) StepProgressGameFile.DownloadFileDone;

                        var add = Convert.ToInt32(
                            Math.Round((double) DownloadProgress.ProgressPercentage / 100 * (max - min),
                                MidpointRounding.ToEven));

                        return (int) StepProgressGameFile.DownloadFile + add;
                    }
                    case StepProgressGameFile.DownloadFileDone:
                        return (int) StepProgressGameFile.DownloadFileDone;
                    case StepProgressGameFile.CheckDownloadFileCrc:
                        return (int) StepProgressGameFile.CheckDownloadFileCrc;
                    case StepProgressGameFile.CheckDownloadFileCrcDone:
                        return (int) StepProgressGameFile.CheckDownloadFileCrcDone;
                    case StepProgressGameFile.ExtractDownloadFile:
                    {
                        if (DownloadProgress == null)
                            return (int) StepProgressGameFile.ExtractDownloadFile;

                        const int min = (int) StepProgressGameFile.ExtractDownloadFile;
                        const int max = (int) StepProgressGameFile.ExtractDownloadFileDone;

                        var add = Convert.ToInt32(
                            Math.Round((double) ExtractProgress.ProgressPercentage / 100 * (max - min),
                                MidpointRounding.ToEven));

                        return (int) StepProgressGameFile.DownloadFile + add;
                    }
                    case StepProgressGameFile.ExtractDownloadFileDone:
                        return (int) StepProgressGameFile.ExtractDownloadFileDone;
                    case StepProgressGameFile.CheckExtractDownloadFileCrc:
                        return (int) StepProgressGameFile.CheckExtractDownloadFileCrc;
                    case StepProgressGameFile.CheckExtractDownloadFileCrcDone:
                        return (int) StepProgressGameFile.CheckExtractDownloadFileCrcDone;
                    case StepProgressGameFile.CopyNewFile:
                        return (int) StepProgressGameFile.CopyNewFile;
                    case StepProgressGameFile.CleanUpTempFile:
                        return (int) StepProgressGameFile.CleanUpTempFile;
                    case StepProgressGameFile.End:
                        return (int) StepProgressGameFile.End;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public StepProgressGameFile StepProgressGameFile { get; }

        public ExLog ProgressLog { get; }

        public ExDownloadProgress DownloadProgress { get; }

        public ExExtractProgress ExtractProgress { get; }
    }

    public class ExDownloadProgress
    {
        public ExDownloadProgress(double totalMilliseconds, int progressPercentage, long bytesReceived,
            long totalBytesToReceive)
        {
            TotalMilliseconds = totalMilliseconds;
            ProgressPercentage = progressPercentage;
            BytesReceived = bytesReceived;
            TotalBytesToReceive = totalBytesToReceive;
        }

        public double TotalMilliseconds { get; }

        public int ProgressPercentage { get; }

        public long BytesReceived { get; }

        public long TotalBytesToReceive { get; }
    }

    public class ExExtractProgress
    {
        public ExExtractProgress(double totalMilliseconds, int progressPercentage, long bytesExtracted,
            long totalBytesToExtract)
        {
            TotalMilliseconds = totalMilliseconds;
            ProgressPercentage = progressPercentage;
            BytesExtracted = bytesExtracted;
            TotalBytesToExtract = totalBytesToExtract;
        }

        public double TotalMilliseconds { get; }

        public int ProgressPercentage { get; }

        public long BytesExtracted { get; }

        public long TotalBytesToExtract { get; }
    }
}