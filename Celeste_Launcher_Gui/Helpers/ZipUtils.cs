using System;
using System.IO.Compression;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ZipUtils
    {
        public static async Task ExtractZipFile(string archiveFileName, string outFolder,
            IProgress<double> progress, CancellationToken ct)
        {
            using (var zipFile = ZipFile.OpenRead(archiveFileName))
            {
                foreach (var zipEntry in zipFile.Entries)
                {
                    var length = zipEntry.Length;

                    var filePath = Path.Combine(outFolder, zipEntry.Name);
                    var directoryName = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(directoryName))
                        Directory.CreateDirectory(directoryName);

                    using (var a = zipEntry.Open())
                    {
                        using (var fileStreamFinal =
                            File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (var final = new BinaryWriter(fileStreamFinal))
                            {
                                var buffer = new byte[4096];
                                int read;
                                var totalread = 0L;
                                while ((read = a.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    //
                                    if (ct.IsCancellationRequested)
                                    {
                                        ct.ThrowIfCancellationRequested();
                                    }

                                    //
                                    if (read > zipEntry.Length)
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
                                        var leftToRead = length - totalread;
                                        totalread += leftToRead;
                                        final.Write(buffer, 0, Convert.ToInt32(leftToRead));
                                    }

                                    //
                                    progress?.Report((double)totalread / length * 100);

                                    //
                                    if (totalread >= length)
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            await Task.Delay(200, ct).ConfigureAwait(false);
        }
    }
}