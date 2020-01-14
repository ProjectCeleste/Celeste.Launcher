﻿#region Using directives

using System;
using System.IO.Compression;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ZipUtils
    {
        public static async Task ExtractZipFile(string archiveFileName, string outFolder,
            IProgress<double> progress, CancellationToken ct)
        {
            using (ZipArchive zipFile = ZipFile.OpenRead(archiveFileName))
            {
                foreach (ZipArchiveEntry zipEntry in zipFile.Entries)
                {
                    long length = zipEntry.Length;

                    string filePath = Path.Combine(outFolder, zipEntry.Name);
                    string directoryName = Path.GetDirectoryName(filePath);

                    if (!string.IsNullOrEmpty(directoryName))
                        Directory.CreateDirectory(directoryName);

                    using (Stream a = zipEntry.Open())
                    {
                        using (FileStream fileStreamFinal =
                            File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (BinaryWriter final = new BinaryWriter(fileStreamFinal))
                            {
                                byte[] buffer = new byte[4096];
                                int read;
                                long totalread = 0L;
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
                                        long leftToRead = length - totalread;
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