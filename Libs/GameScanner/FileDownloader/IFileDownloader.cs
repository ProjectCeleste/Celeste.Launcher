using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCeleste.GameFiles.GameScanner.FileDownloader
{
    public interface IFileDownloader
    {
        double DownloadProgress { get; }
        long DownloadSize { get; }
        long BytesDownloaded { get; }
        string DownloadUrl { get; }
        double DownloadSpeed { get; }
        string FilePath { get; }
        Exception Error { get; }
        FileDownloaderState State { get; }

        event EventHandler ProgressChanged;

        Task DownloadAsync(CancellationToken ct = default);
    }
}