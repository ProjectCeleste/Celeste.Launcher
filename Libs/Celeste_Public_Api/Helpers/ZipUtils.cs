#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using Ionic.Zlib;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class ZipFileProgress
    {
        public ZipFileProgress(string inputFileName, string outputFileName, double totalMilliseconds,
            long bytesExtracted, long totalBytesToExtract)
        {
            InputFileName = inputFileName;
            OutputFileName = outputFileName;
            TotalMilliseconds = totalMilliseconds;
            BytesProcessed = bytesExtracted;
            TotalBytesToProcess = totalBytesToExtract;
        }

        public string InputFileName { get; }

        public string OutputFileName { get; }

        public double TotalMilliseconds { get; }

        public int ProgressPercentage => Convert.ToInt32(
            Math.Floor((double) BytesProcessed / TotalBytesToProcess * 100));

        public long BytesProcessed { get; }

        public long TotalBytesToProcess { get; }
    }

    public static class ZipUtils
    {
        public static async Task DoExtractZipFile(string archiveFileName, string outFolder,
            IProgress<ZipFileProgress>progress, CancellationToken ct,
            string password = null)
        {
            using (var zip = new ZipFile(archiveFileName))
            {
                if (!string.IsNullOrEmpty(password))
                    zip.Password = password;
                foreach (var zipEntry in zip)
                {
                    if (zipEntry.IsDirectory)
                        continue;

                    var length = zipEntry.UncompressedSize;

                    var filePath = Path.Combine(outFolder, zipEntry.FileName);
                    var directoryName = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(directoryName))
                        Directory.CreateDirectory(directoryName);

                    using (var a = new MemoryStream())
                    {
                        if (!string.IsNullOrEmpty(password))
                            zipEntry.ExtractWithPassword(a, password);
                        else
                            zipEntry.Extract(a);

                        a.Position = 0;

                        using (var fileStreamFinal =
                            File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (var final = new BinaryWriter(fileStreamFinal))
                            {
                                var buffer = new byte[4096];
                                int read;
                                var totalread = 0L;
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
                                    if (read > zipEntry.UncompressedSize)
                                    {
                                        totalread += length;
                                        final.Write(buffer, 0, Convert.ToInt32(length));
                                    }
                                    else if (totalread + read <= length)
                                    {
                                        totalread += read;
                                        final.Write(buffer, 0, read);
                                    }
                                    else if (totalread + read > length)
                                    {
                                        totalread += length - totalread;
                                        final.Write(buffer, 0, Convert.ToInt32(length - totalread));
                                    }

                                    //
                                    progress?.Report(new ZipFileProgress(archiveFileName, filePath,
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
            await Task.Delay(200, ct).ConfigureAwait(false);
        }

        #region L33T Zip

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

        public static async Task DoExtractL33TZipFile(string fileName, string outputFileName,
            IProgress<ZipFileProgress> progress, CancellationToken ct)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();

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
                            using (var fileStreamFinal =
                                File.Open(outputFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                using (var final = new BinaryWriter(fileStreamFinal))
                                {
                                    var buffer = new byte[4096];
                                    int read;
                                    var totalread = 0;
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
                                        progress?.Report(new ZipFileProgress(fileName, outputFileName,
                                            stopwatch.Elapsed.TotalMilliseconds, totalread,
                                            length));

                                        //
                                        if (totalread >= length)
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (AggregateException)
            {
                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

                throw;
            }
            finally
            {
                stopwatch.Stop();
            }

            await Task.Delay(200, ct).ConfigureAwait(false);
        }


        public static async Task DoCreateL33TZipFile(string inputFileName, string outputFileName,
            IProgress<ZipFileProgress> progress, CancellationToken ct,
            CompressionLevel compressionLevel = CompressionLevel.BestCompression)
        {
            if (!File.Exists(inputFileName))
                throw new FileNotFoundException($"File '{inputFileName}' not found!", inputFileName);

            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();

                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

                var length = Convert.ToInt32(new FileInfo(inputFileName).Length);

                using (var fileStream = File.Open(inputFileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (var fileStreamFinal =
                        File.Open(outputFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (var final = new BinaryWriter(fileStreamFinal))
                        {
                            //
                            final.BaseStream.Position = 0L;

                            //Write L33T Header
                            char[] l33T = {'l', '3', '3', 't'};
                            final.Write(l33T);

                            //Write File Length
                            final.Write(length);

                            //Write Deflate specification (2 Byte)
                            final.Write(new byte[] {0x78, 0x9C});

                            //
                            using (var a = new DeflateStream(fileStreamFinal, CompressionMode.Compress,
                                compressionLevel))
                            {
                                var buffer = new byte[4096];
                                var totalread = 0;
                                int read;
                                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    //
                                    if (ct.IsCancellationRequested)
                                    {
                                        if (stopwatch.IsRunning)
                                            stopwatch.Stop();
                                        ct.ThrowIfCancellationRequested();
                                    }

                                    //
                                    if (read > length)
                                    {
                                        totalread += length;
                                        a.Write(buffer, 0, length);
                                    }
                                    else if (totalread + read <= length)
                                    {
                                        totalread += read;
                                        a.Write(buffer, 0, read);
                                    }
                                    else if (totalread + read > length)
                                    {
                                        totalread += length - totalread;
                                        a.Write(buffer, 0, length - totalread);
                                    }

                                    //
                                    progress?.Report(new ZipFileProgress(inputFileName, outputFileName,
                                        stopwatch.Elapsed.TotalMilliseconds, totalread,
                                        length));

                                    //
                                    if (totalread >= length)
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (AggregateException)
            {
                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

                throw;
            }
            finally
            {
                if (stopwatch.IsRunning)
                    stopwatch.Stop();
            }

            await Task.Delay(200, ct).ConfigureAwait(false);
        }

        #endregion
    }
}