#region Using directives

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Force.Crc32;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Misc
{
    public static class Crc32Utils
    {
        public static uint GetCrc32FromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

            using (var fs = File.OpenRead(fileName))
            {
                var result = 0u;
                var buffer = new byte[4096];
                int read;
                var totalread = 0L;
                var length = fs.Length;
                while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    totalread += read;

                    result = Crc32Algorithm.Append(result, buffer, 0, read);

                    if (totalread >= length)
                        break;
                }

                return result;
            }
        }

        public static async Task<uint> DoGetCrc32FromFile(string fileName,
            CancellationToken ct = default(CancellationToken),
            IProgress<double> progress = null)
        {
            return await Task.Run(() =>
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

                using (var fs = File.OpenRead(fileName))
                {
                    var result = 0u;
                    var buffer = new byte[4096];
                    int read;
                    var totalread = 0L;
                    var length = fs.Length;
                    var lastProgress = 0d;

                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //
                        ct.ThrowIfCancellationRequested();

                        totalread += read;

                        result = Crc32Algorithm.Append(result, buffer, 0, read);

                        var newProgress = (double)totalread / length * 100;

                        if (newProgress - lastProgress > 1)
                        {
                            progress?.Report(newProgress);
                            lastProgress = newProgress;
                        }

                        if (totalread >= length)
                            break;
                    }

                    return result;
                }
            }, ct);
        }
    }
}