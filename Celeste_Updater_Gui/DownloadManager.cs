#region Using directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Ionic.Zlib;

#endregion

namespace Celeste_Updater_Gui
{
    public class DownloadProgress
    {
        public double TotalMilliseconds { get; set; }

        public int ProgressPercentage { get; set; }

        public long BytesReceived { get; set; }

        public long TotalBytesToReceive { get; set; }
    }

    public enum DownloadState
    {
        Unknow = 0,
        Idle = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public class DownloadManager
    {
        public DownloadState DownloadState { get; private set; } = DownloadState.Idle;

        public DownloadProgress DownloadProgress { get; } = new DownloadProgress();

        private readonly Stopwatch Stopwatch = new Stopwatch();

        private readonly string _tempFileName = Path.GetTempFileName();

        private string _httpLink;

        public string FileName { get; set; }

        public string HttpLink
        {
            get => _httpLink;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _httpLink = value.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                        ? value
                        : $"http://{value}";
                else
                    _httpLink = value;
            }
        }

        public int Crc32 { get; set; }

        public void DownloadFile()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFileCompleted += Completed;
                    webClient.DownloadProgressChanged += ProgressChanged;
                    DownloadState = DownloadState.InProgress;
                    Stopwatch.Start();
                    webClient.DownloadFileAsync(new Uri(HttpLink), _tempFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgress.ProgressPercentage = e.ProgressPercentage;
            DownloadProgress.BytesReceived = e.BytesReceived;
            DownloadProgress.TotalBytesToReceive = e.TotalBytesToReceive;
            DownloadProgress.TotalMilliseconds = Stopwatch.Elapsed.TotalMilliseconds;

        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Stopwatch.Stop();
            DownloadState = e.Cancelled ? DownloadState.Cancelled : DownloadState.Completed;
        }

        public static bool IsL33TCompressedFile(string fileName)
        {
            bool result;
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    reader.BaseStream.Position = 0L;
                    var head = new string(reader.ReadChars(4));
                    result = head == "l33t";
                }
            }
            return result;
        }

        public byte[] GetDecompressedByte(string fileName)
        {
            byte[] output;
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    reader.BaseStream.Position = 0L;
                    //
                    var head = new string(reader.ReadChars(4));
                    if (head.ToLower() != "l33t")
                        throw new FileLoadException($"'l33t' header not found, file: '{fileName}'");
                    var length = reader.ReadInt32();
                    //
                    reader.BaseStream.Position = 8L;
                    using (var a = new DeflateStream(reader.BaseStream, CompressionMode.Decompress))
                    {
                        using (var final = new MemoryStream())
                        {
                            var buffer = new byte[length];
                            var read = a.Read(buffer, 0, buffer.Length);
                            if (read > 0 && read == length)
                                final.Write(buffer, 0, read);
                            else
                                throw new FileLoadException($"read {read} != length {length}, file: '{fileName}'");
                            output = final.ToArray();
                        }
                    }
                }
            }

            return output;
        }
    }
}