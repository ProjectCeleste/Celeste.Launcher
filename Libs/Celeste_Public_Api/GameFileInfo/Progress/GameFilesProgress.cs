#region Using directives

using System;

#endregion

namespace Celeste_Public_Api.GameFileInfo.Progress
{
    public class ExProgressGameFiles
    {
        public ExProgressGameFiles(int totalFile, int currentIndex,
            ExProgressGameFile progressGameFile)
        {
            ProgressPercentage = Convert.ToInt32(
                Math.Round((double) currentIndex / totalFile * 100,
                    MidpointRounding.ToEven));
            TotalFile = totalFile;
            CurrentIndex = currentIndex;
            ProgressGameFile = progressGameFile;
        }

        public int ProgressPercentage { get; }

        public int TotalFile { get; }

        public int CurrentIndex { get; }

        public ExProgressGameFile ProgressGameFile { get; }
    }
}