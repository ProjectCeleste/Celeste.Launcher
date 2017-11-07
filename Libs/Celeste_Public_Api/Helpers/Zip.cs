#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using Ionic.Zlib;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class ZipFileProgress
    {
        public ZipFileProgress(string fileName, string outputFileName, double totalMilliseconds,
            long bytesExtracted, long totalBytesToExtract)
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
            Math.Floor((double)BytesExtracted / TotalBytesToExtract * 100));

        public long BytesExtracted { get; }

        public long TotalBytesToExtract { get; }
    }

    public static class Zip
    {
        public static byte[] Compress(byte[] input,
            CompressionLevel compressionLevel = CompressionLevel.BestCompression)
        {
            byte[] output;
            using (var final = new MemoryStream())
            {
                using (var a = new DeflateStream(final, CompressionMode.Compress, compressionLevel))
                {
                    a.Write(input, 0, input.Length);
                }
                output = final.ToArray();
            }
            return output;
        }

        public static byte[] Decompress(byte[] input)
        {
            byte[] output;
            using (var f = new MemoryStream(input))
            {
                using (var a = new DeflateStream(f, CompressionMode.Decompress))
                {
                    using (var final = new MemoryStream())
                    {
                        var buffer = new byte[1024 * 1024];
                        int read;
                        while ((read = a.Read(buffer, 0, buffer.Length)) > 0)
                            final.Write(buffer, 0, read);

                        output = final.ToArray();
                    }
                }
            }
            return output;
        }

        public static void ZipDirectory(string directory, string outFileName)
        {
            ZipDirectory(directory, outFileName, CompressionLevel.BestCompression);
        }

        public static void ZipDirectory(string directory, string outFileName, CompressionLevel compressionLevel)
        {
            if (directory.EndsWith($"{Path.DirectorySeparatorChar}"))
                directory = directory.Substring(0, directory.Length - 1);

            using (var zip = new ZipFile {CompressionLevel = compressionLevel})
            {
                foreach (var f in Directory.GetFiles(directory, "*",
                    SearchOption.AllDirectories).ToArray())
                {
                    var directoryName = Path.GetDirectoryName(f);
                    if (directoryName == null) continue;
                    zip.AddFile(f, directoryName.Replace(directory, string.Empty));
                }
                zip.Save(outFileName);
            }
        }

        public static async Task DoExtractZipFile(string archiveFileName, string outFolder, IProgress<ZipFileProgress>progress, CancellationToken ct,
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

                        using (var a = new DeflateStream(zipEntry.InputStream, CompressionMode.Decompress))
                        {
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
                                            final.Write(buffer, 0, Convert.ToInt32(length -totalread));
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
                                        var buffer = new byte[4096];
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
                                            progress?.Report(new ZipFileProgress(fileName, outputFileName,
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
                    if (File.Exists(outputFileName))
                        File.Delete(outputFileName);

                    throw;
            }

            await Task.Delay(200, ct).ConfigureAwait(false);
        }

    }
}