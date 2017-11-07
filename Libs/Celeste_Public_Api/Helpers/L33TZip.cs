#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zlib;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class L33TZipExtractProgress
    {
        public L33TZipExtractProgress(string fileName, string outputFileName, double totalMilliseconds,
            long bytesExtracted,
            long totalBytesToExtract)
        {
            FileName = fileName;
            OutputFileName = outputFileName;
            TotalMilliseconds = totalMilliseconds;
            BytesExtracted = bytesExtracted;
            TotalBytesToExtract = totalBytesToExtract;
        }

        public string FileName { get; }

        public string OutputFileName { get; }

        public double TotalMilliseconds { get; }

        public int ProgressPercentage => Convert.ToInt32(
            Math.Floor((double) BytesExtracted / TotalBytesToExtract * 100));

        public long BytesExtracted { get; }

        public long TotalBytesToExtract { get; }
    }

    public class L33TZip
    {
        public static bool IsL33TZipFile(string fileName)
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

        public static async Task ExtractL33TZipFile(string fileName, string outputFileName,
            IProgress<L33TZipExtractProgress> progress, CancellationToken ct)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!File.Exists(fileName))
                        throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

                    if (File.Exists(outputFileName))
                        File.Delete(outputFileName);

                    using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        using (var reader = new BinaryReader(fileStream))
                        {
                            reader.BaseStream.Position = 0L;

                            //Header
                            var head = new string(reader.ReadChars(4));
                            if (head.ToLower() != "l33t")
                                throw new FileLoadException($"'l33t' header not found, file: '{fileName}'");
                            
                            //Length
                            var length = reader.ReadInt32();

                            //Skip deflate specification (2 Byte)
                            reader.BaseStream.Position = 10L;

                            //
                            using (var a = new DeflateStream(reader.BaseStream, CompressionMode.Decompress))
                            {
                                using (var fileStreamFinal = File.Open(outputFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    using (var final = new BinaryWriter(fileStreamFinal))
                                    {
                                        var buffer = new byte[8192];
                                        int read;
                                        var totalread = 0;
                                        var stopwatch = new Stopwatch();
                                        stopwatch.Start();
                                        while ((read = a.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            //
                                            if (ct.IsCancellationRequested)
                                            {
                                                stopwatch.Stop();
                                                ct.ThrowIfCancellationRequested();
                                            }

                                            //
                                            if (read > length)
                                            {
                                                totalread += length;
                                                final.Write(buffer, 0, length);
                                            }
                                            else if (totalread + read <= length)
                                            {
                                                totalread += read;
                                                final.Write(buffer, 0, read);
                                            }
                                            else if (totalread + read > length)
                                            {
                                                totalread += length - totalread;
                                                final.Write(buffer, 0, length - totalread);
                                            }

                                            //
                                            progress?.Report(new L33TZipExtractProgress(fileName, outputFileName,
                                                stopwatch.Elapsed.TotalMilliseconds, totalread,
                                                length));

                                            //
                                            if (totalread >= length)
                                                break;
                                        }
                                        stopwatch.Stop();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (AggregateException)
                {
                    if(File.Exists(outputFileName))
                        File.Delete(outputFileName);

                    throw;
                }
            }, ct);
        }
    }
}