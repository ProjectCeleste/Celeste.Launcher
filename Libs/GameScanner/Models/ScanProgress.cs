using System;

namespace ProjectCeleste.GameFiles.GameScanner.Models
{
    public class ScanProgress
    {
        public ScanProgress(string file, double progressPercentage, int index, int totalIndex)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
            ProgressPercentage = progressPercentage;
            Index = index;
            TotalIndex = totalIndex;
        }

        public string File { get; }

        public double ProgressPercentage { get; }

        public int Index { get; }

        public int TotalIndex { get; }
    }
}