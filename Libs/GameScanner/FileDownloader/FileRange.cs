namespace ProjectCeleste.GameFiles.GameScanner.FileDownloader
{
    internal class FileRange
    {
        public FileRange(long start, long end)
        {
            Start = start;
            End = end;
        }

        public long Start { get; }

        public long End { get; }
    }
}