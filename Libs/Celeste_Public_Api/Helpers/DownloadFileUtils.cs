#region Using directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public enum DownloadState
    {
        Unknow = 0,
        Idle = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        Failed = 5
    }

    public class DownloadFileProgress
    {
        public DownloadFileProgress(double totalMilliseconds, int progressPercentage, long bytesReceived,
            long totalBytesToReceive)
        {
            TotalMilliseconds = totalMilliseconds;
            ProgressPercentage = progressPercentage;
            BytesReceived = bytesReceived;
            TotalBytesToReceive = totalBytesToReceive;
        }

        public double TotalMilliseconds { get; }

        public int ProgressPercentage { get; }

        public long BytesReceived { get; }

        public long TotalBytesToReceive { get; }
    }

    public class DownloadFileUtils
    {
        private readonly Stopwatch _stopwatch;

        public DownloadFileUtils(Uri httpLink, string outputFileName, IProgress<DownloadFileProgress> progress = null)
        {
            HttpLink = httpLink;
            OutputFileName = outputFileName;
            Progress = progress;
            _stopwatch = new Stopwatch();
        }

        private DownloadState DownloadState { get; set; } = DownloadState.Idle;

        private Uri HttpLink { get; }

        private string OutputFileName { get; }

        private IProgress<DownloadFileProgress> Progress { get; }

        public async Task DoDownload(CancellationToken ct)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                await Task.Run(() =>
                {
                    using (var webClient = new WebClient())
                    {
                        //
                        webClient.DownloadFileCompleted += Completed;
                        webClient.DownloadProgressChanged += ProgressChanged;

                        //
                        Progress?.Report(new DownloadFileProgress(0, 0, 0, 0));

                        //
                        _stopwatch.Start();
                        DownloadState = DownloadState.InProgress;

                        //
                        webClient.DownloadFileAsync(HttpLink, OutputFileName, ct);

                        //
                        while (DownloadState == DownloadState.InProgress)
                        {
                            if (!ct.IsCancellationRequested)
                                continue;

                            _stopwatch.Stop();
                            webClient.CancelAsync();
                        }

                        if (DownloadState != DownloadState.Completed)
                            throw new WebException("Download Failed!");
                    }
                }, ct);
            }
            catch (AggregateException)
            {
                if (DownloadState != DownloadState.Cancelled)
                    DownloadState = DownloadState.Failed;

                throw;
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress?.Report(new DownloadFileProgress(_stopwatch.Elapsed.TotalMilliseconds, e.ProgressPercentage,
                e.BytesReceived,
                e.TotalBytesToReceive));
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            //
            _stopwatch.Stop();

            //
            if (e.Error != null)
            {
                DownloadState = DownloadState.Failed;
                return;
            }
            //
            DownloadState = e.Cancelled ? DownloadState.Cancelled : DownloadState.Completed;
        }

        /// <summary>
        ///     Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain,
            SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == SslPolicyErrors.None)
                return true;

            MessageBox.Show($"X509Certificate [{cert.Subject}]\r\n" +
                            "Policy Error:\r\n" +
                            $"'{error}'", "", MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return false;
        }
    }
}