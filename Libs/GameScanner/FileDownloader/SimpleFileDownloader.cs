using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCeleste.GameFiles.GameScanner.FileDownloader
{
    public class SimpleFileDownloader : IFileDownloader
    {
        private readonly Stopwatch _stopwatch;

        public SimpleFileDownloader(string httpLink, string outputFileName)
        {
            DownloadUrl = httpLink;
            FilePath = outputFileName;
            _stopwatch = new Stopwatch();
        }

        public FileDownloaderState State { get; private set; } = FileDownloaderState.Invalid;

        public double DownloadProgress { get; private set; }

        public long DownloadSize { get; private set; }

        public long BytesDownloaded { get; private set; }

        public string DownloadUrl { get; }

        public double DownloadSpeed { get; private set; }

        public string FilePath { get; }

        public Exception Error { get; private set; }

        public async Task DownloadAsync(CancellationToken ct = default)
        {
            if (State == FileDownloaderState.Download)
                return;

            //
            State = FileDownloaderState.Download;

            var path = Path.GetDirectoryName(FilePath);
            if (path != null && !Directory.Exists(path))
                Directory.CreateDirectory(path);

            using var webClient = new WebClient();
            //
            webClient.DownloadFileCompleted += DownloadCompleted;
            webClient.DownloadProgressChanged += DownloadProgressChanged;

            //
            var cancel = ct.Register(() =>
            {
                _stopwatch.Stop();
                webClient.CancelAsync();
            }, true);

            _stopwatch.Reset();
            _stopwatch.Start();
            using (new Timer(ReportProgress, null, 500, 500))
            {
                try
                {
                    await webClient.DownloadFileTaskAsync(DownloadUrl, FilePath);
                    if (BytesDownloaded == DownloadSize)
                        State = FileDownloaderState.Complete;
                }
                finally
                {
                    _stopwatch.Stop();
                    webClient.CancelAsync();
                    cancel.Dispose();
                }
            }

            switch (State)
            {
                case FileDownloaderState.Error:
                    throw Error;
                case FileDownloaderState.Abort:
                    throw new OperationCanceledException(ct);
                case FileDownloaderState.Complete:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State), State, null);
            }
        }

        public event EventHandler ProgressChanged;

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadSize = e.TotalBytesToReceive;
            BytesDownloaded = e.BytesReceived;
            DownloadProgress = e.ProgressPercentage;
            DownloadSpeed = (double) e.BytesReceived / _stopwatch.Elapsed.Seconds;
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //
            if (e.Error != null)
            {
                Error = e.Error;
                State = FileDownloaderState.Error;
            }
            else if (e.Cancelled)
            {
                Error = new OperationCanceledException();
                State = FileDownloaderState.Abort;
            }
            else
            {
                Error = null;
                DownloadProgress = 100;
                BytesDownloaded = DownloadSize;
                State = FileDownloaderState.Complete;
            }
        }

        protected virtual void OnProgressChanged()
        {
            ProgressChanged?.Invoke(this, null);
        }

        private void ReportProgress(object state)
        {
            OnProgressChanged();
        }
    }
}