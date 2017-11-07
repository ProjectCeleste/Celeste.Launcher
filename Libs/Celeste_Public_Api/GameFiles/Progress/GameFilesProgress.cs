#region Using directives

using System;

#endregion

namespace Celeste_Public_Api.GameFiles.Progress
{
    public class ExProgressGameFiles
    {
        public ExProgressGameFiles(int totalFile, int currentIndex,
            ScanAndRepairFileProgress progressGameFile)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double) currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            ScanAndRepairFileProgress = progressGameFile;
        }

        public int ProgressPercentage { get; }

        public int TotalFile { get; }

        public int CurrentIndex { get; }

        public ScanAndRepairFileProgress ScanAndRepairFileProgress { get; }
    }

    public class ExProgressGameFilesQ
    {
        public ExProgressGameFilesQ(int totalFile, int currentIndex,
            string fileName)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double)currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            FileName = fileName;
        }

        public int ProgressPercentage { get; }

        public int TotalFile { get; }

        public int CurrentIndex { get; }

        public string FileName { get; }
    }
}