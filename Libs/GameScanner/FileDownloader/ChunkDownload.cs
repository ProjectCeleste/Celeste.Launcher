using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCeleste.GameFiles.GameScanner.FileDownloader
{
    internal class ChunkDownload
    {
        internal const int ChunkBufferSize = 32 * 1024; // 32Kb

        internal string DownloadTmpFileName { get; }

        private readonly string _fileToDownload;
        private readonly FileRange _fileRange;

        private int _bytesDownloaded;

        internal ChunkDownload(string fileToDownload, FileRange fileRange, string tmpFolder)
        {
            _fileRange = fileRange;
            _fileToDownload = fileToDownload;

            DownloadTmpFileName =
                Path.Combine(tmpFolder,
                    $"0x{fileToDownload.ToLower().GetHashCode():X4}.0x{fileRange.Start:X8}.tmp");
        }

        private HttpWebRequest CreateHttpWebRequest()
        {
            var downloadRequest = WebRequest.CreateHttp(_fileToDownload);
            downloadRequest.AllowAutoRedirect = true;
            downloadRequest.ServicePoint.ConnectionLimit = 100;
            downloadRequest.ServicePoint.Expect100Continue = false;

            downloadRequest.AddRange(_fileRange.Start, _fileRange.End);

            return downloadRequest;
        }

        internal async Task<bool> TryDownloadAsync(Action<int> progressCallback, CancellationToken ct = default)
        {
            var downloadRequest = CreateHttpWebRequest();

            try
            {
                using var downloadResponse = (HttpWebResponse) downloadRequest.GetResponse();
                using var downloadSource = downloadResponse.GetResponseStream();
                using var downloadTarget = new FileStream(DownloadTmpFileName, FileMode.Create, FileAccess.Write);
                int bytesRead;
                var buffer = new byte[ChunkBufferSize];

                do
                {
                    bytesRead = await downloadSource.ReadAsync(buffer, 0, ChunkBufferSize, ct);
                    await downloadTarget.WriteAsync(buffer, 0, bytesRead, ct);

                    progressCallback(bytesRead);
                    _bytesDownloaded += bytesRead;
                } while (bytesRead > 0);
            }
            catch
            {
                File.Delete(DownloadTmpFileName);
                progressCallback(-_bytesDownloaded);
                return false;
            }
            finally
            {
                downloadRequest.Abort();
            }

            return true;
        }
    }
}