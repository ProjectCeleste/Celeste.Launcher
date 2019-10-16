#region Using directives

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class ZipUtils
    {
        public static async Task ExtractZipFile(string archiveFileName, string outFolder,
            CancellationToken ct = default(CancellationToken),
            string password = null, IProgress<double> progress = null)
        {
            await Task.Run(() =>
            {
                using (var zip = new ZipFile(archiveFileName))
                {
                    if (!string.IsNullOrEmpty(password))
                        zip.Password = password;

                    foreach (var zipEntry in zip)
                    {
                        //
                        ct.ThrowIfCancellationRequested();

                        //
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
                                    while ((read = a.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        //
                                        ct.ThrowIfCancellationRequested();

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
                                            var leftToRead = length - totalread;
                                            totalread += leftToRead;
                                            final.Write(buffer, 0, Convert.ToInt32(leftToRead));
                                        }

                                        //
                                        progress?.Report((double) totalread / length * 100);

                                        //
                                        if (totalread >= length)
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }, ct);
        }
    }
}