namespace ProjectCeleste.GameFiles.GameScanner.FileDownloader
{
    public enum FileDownloaderState
    {
        Invalid,
        Download,
        Finalize,
        Complete,
        Error,
        Abort
    }
}