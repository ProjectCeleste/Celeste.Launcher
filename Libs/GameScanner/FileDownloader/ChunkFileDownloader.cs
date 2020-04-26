using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCeleste.GameFiles.GameScanner.FileDownloader
{
    public class ChunkFileDownloader : IFileDownloader
    {
        internal const int MaxChunkSize = 10 * 1024 * 1024; //10Mb
        public const int MaxConcurrentDownloads = 10;

        private readonly int _concurrentDownloads;
        private readonly Stopwatch _downloadSpeedStopwatch;
        private readonly string _downloadTempFolder;
        private readonly ConcurrentDictionary<long, string> _completedChunks = new ConcurrentDictionary<long, string>();

        private ConcurrentQueue<FileRange> _chunkDownloadQueue;
        private long _downloadSizeCompleted;

        public ChunkFileDownloader(string httpLink, string outputFileName, string tmpFolder, int concurrentDownload = 0)
        {
            DownloadUrl = httpLink;
            FilePath = outputFileName;

            if (concurrentDownload <= 0)
                concurrentDownload = 5;

            if (concurrentDownload > MaxConcurrentDownloads)
                concurrentDownload = MaxConcurrentDownloads;

            _concurrentDownloads = concurrentDownload;

            _downloadTempFolder = tmpFolder;
            _downloadSpeedStopwatch = new Stopwatch();

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.MaxServicePointIdleTime = 1000;
        }

        public FileDownloaderState State { get; private set; }

        public double DownloadProgress => DownloadSize > 0 ? (double) BytesDownloaded / DownloadSize * 100 : 0;

        public long DownloadSize { get; private set; }

        public long BytesDownloaded => Interlocked.Read(ref _downloadSizeCompleted);

        public string DownloadUrl { get; }

        public double DownloadSpeed
        {
            get
            {
                double receivedBytes = BytesDownloaded;
                return receivedBytes > 0
                    ? receivedBytes / ((double) _downloadSpeedStopwatch.ElapsedMilliseconds / 1000)
                    : 0;
            }
        }

        public string FilePath { get; }

        public Exception Error { get; private set; }

        public async Task DownloadAsync(CancellationToken ct = default)
        {
            if (State == FileDownloaderState.Download)
                return;

            State = FileDownloaderState.Download;
            Interlocked.Exchange(ref _downloadSizeCompleted, 0);
            _downloadSpeedStopwatch.Reset();

            var downloadDirectoryName = Path.GetDirectoryName(FilePath);
            if (downloadDirectoryName != null && !Directory.Exists(downloadDirectoryName))
                Directory.CreateDirectory(downloadDirectoryName);

            if (!Directory.Exists(_downloadTempFolder))
                Directory.CreateDirectory(_downloadTempFolder);
            try
            {
                _downloadSpeedStopwatch.Start();
                DownloadSize = await GetDownloadSizeAsync();

                ReportProgress(null); //Forced

                using (var reportProgressTimer = new Timer(ReportProgress, null, 500, 500))
                {
                    await StartDownloadAsync(ct);
                    _downloadSpeedStopwatch.Stop();
                    reportProgressTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }

                ReportProgress(null); //Forced

                if (_chunkDownloadQueue.Count > 0)
                    throw new Exception(
                        $"Download was incomplete ({_chunkDownloadQueue.Count} missing chunks)");

                if (BytesDownloaded != DownloadSize)
                    throw new Exception(
                        $"Download was incomplete ({BytesDownloaded}/{DownloadSize} bytes)");

                State = FileDownloaderState.Finalize;
                
                ReportProgress(null); //Forced

                ct.ThrowIfCancellationRequested();

                WriteChunksToFile(_completedChunks);

                State = FileDownloaderState.Complete;
            }
            catch (Exception e)
            {
                _downloadSpeedStopwatch.Stop();
                Error = e;
                State = e is OperationCanceledException ? FileDownloaderState.Abort : FileDownloaderState.Error;
                throw;
            }
            finally
            {
                ReportProgress(null); //Forced
            }
        }

        private async Task StartDownloadAsync(CancellationToken ct = default)
        {
            var readRanges = CalculateFileChunkRanges();
            var fileRanges = readRanges as FileRange[] ?? readRanges.ToArray();
            _chunkDownloadQueue = new ConcurrentQueue<FileRange>(fileRanges);

            var tasks = Enumerable.Range(1, Math.Min(_concurrentDownloads, _chunkDownloadQueue.Count)).Select(
                async workerIndex =>
                {
                    if (workerIndex > 1)
                        await Task.Delay(1000 * (workerIndex - 1), ct);

                    await DequeueAndDownloadChunksAsync(ct);
                });

            await Task.WhenAll(tasks); //Start parallel download

            if (_completedChunks.Count > 0 && _chunkDownloadQueue.Count > 0)
            {
                //Try to get missing chunk if any
                await DequeueAndDownloadChunksAsync(ct);
            }
        }

        private async Task DequeueAndDownloadChunksAsync(CancellationToken ct = default)
        {
            while (_chunkDownloadQueue.TryDequeue(out var fileChunk))
            {
                var workerFailedDownloadingOnce = false;

                retry:
                var chunkDownload = new ChunkDownload(DownloadUrl, fileChunk, _downloadTempFolder);
                var downloadSuccessfullyCompleted =
                    await chunkDownload.TryDownloadAsync(IncrementTotalDownloadProgress, ct);

                if (!downloadSuccessfullyCompleted)
                {
                    if (workerFailedDownloadingOnce)
                    {
                        _chunkDownloadQueue.Enqueue(fileChunk);
                        break;
                    }

                    workerFailedDownloadingOnce = true;
                    await Task.Delay(1000, ct);
                    goto retry;
                }

                _completedChunks.TryAdd(fileChunk.Start, chunkDownload.DownloadTmpFileName);

                await Task.Delay(1000, ct);
            }
        }

        private IEnumerable<FileRange> CalculateFileChunkRanges()
        {
            for (var chunkIndex = 0;
                chunkIndex < DownloadSize / MaxChunkSize + (DownloadSize % MaxChunkSize > 0 ? 1 : 0);
                chunkIndex++)
            {
                var chunkStart = MaxChunkSize * chunkIndex;
                var chunkEnd = Math.Min(chunkStart + MaxChunkSize - 1, DownloadSize);

                yield return new FileRange(chunkStart, chunkEnd);
            }
        }

        private async Task<long> GetDownloadSizeAsync()
        {
            var sizeDownloadRequest = WebRequest.Create(DownloadUrl);
            sizeDownloadRequest.Method = "HEAD";

            using var response = await sizeDownloadRequest.GetResponseAsync();
            return long.Parse(response.Headers.Get("Content-Length"));
        }

        private void WriteChunksToFile(IDictionary<long, string> fileChunks)
        {
            using var targetFile = new BufferedStream(new FileStream(FilePath, FileMode.Create, FileAccess.Write),
                ChunkDownload.ChunkBufferSize);
            foreach (var tempFile in fileChunks.ToArray().OrderBy(b => b.Key))
            {
                using (var sourceChunks =
                    new BufferedStream(File.OpenRead(tempFile.Value), ChunkDownload.ChunkBufferSize))
                    sourceChunks.CopyTo(targetFile);

                try
                {
                    File.Delete(tempFile.Value);
                }
                catch
                {
                    //
                }
            }
        }

        private void IncrementTotalDownloadProgress(int bytesDownloaded)
        {
            Interlocked.Add(ref _downloadSizeCompleted, bytesDownloaded);
        }

        public event EventHandler ProgressChanged;

        private void ReportProgress(object state)
        {
            ProgressChanged?.Invoke(this, null);
        }
    }
}