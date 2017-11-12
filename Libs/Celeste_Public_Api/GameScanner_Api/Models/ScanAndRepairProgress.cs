#region Using directives

using System;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Public_Api.GameScanner_Api.Models
{
    public class ScanAndRepairProgress
    {
        public ScanAndRepairProgress(int totalFile, int currentIndex)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double) currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            ScanAndRepairFileProgress = null;
            ProgressLog = null;
        }

        public ScanAndRepairProgress(int totalFile, int currentIndex, ExLog progressLog)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double) currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            ScanAndRepairFileProgress = null;
            ProgressLog = progressLog;
        }

        public ScanAndRepairProgress(int totalFile, int currentIndex,
            ScanAndRepairFileProgress progressGameFile)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double) currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            ScanAndRepairFileProgress = progressGameFile;
            ProgressLog = null;
        }

        public ScanAndRepairProgress(int totalFile, int currentIndex,
            ScanAndRepairFileProgress progressGameFile, ExLog progressLog)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double) currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            ScanAndRepairFileProgress = progressGameFile;
            ProgressLog = progressLog;
        }

        public int ProgressPercentage { get; }

        public int TotalFile { get; }

        public int CurrentIndex { get; }

        public ScanAndRepairFileProgress ScanAndRepairFileProgress { get; }

        public ExLog ProgressLog { get; }
    }
}