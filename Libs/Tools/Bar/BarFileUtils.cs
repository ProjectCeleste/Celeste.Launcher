#region Using directives

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectCeleste.GameFiles.Tools.L33TZip;
using ProjectCeleste.GameFiles.Tools.Xmb;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Bar
{
    public static class BarFileUtils
    {
        public static void ExtractBarFiles(string inputFile, string outputPath, bool convertFile = true)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException($"File '{inputFile}' not found!", inputFile);

            if (!outputPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                outputPath = outputPath + Path.DirectorySeparatorChar;

            BarFile barFilesInfo;
            using (var fileStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var binReader = new BinaryReader(fileStream))
                {
                    //Read Header
                    binReader.BaseStream.Seek(0, SeekOrigin.Begin); //Seek to header
                    var barFileHeader = new BarFileHeader(binReader);

                    //Read Files Info
                    binReader.BaseStream.Seek(barFileHeader.FilesTableOffset, SeekOrigin.Begin); //Seek to file table

                    barFilesInfo = new BarFile(binReader);
                }
            }
            var exceptions = new BlockingCollection<Exception>();
            Parallel.ForEach(barFilesInfo.BarFileEntrys.ToArray(), barFileInfo =>
            {
                try
                {
                    using (var fileStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (var binReader = new BinaryReader(fileStream))
                        {
                            binReader.BaseStream.Seek(barFileInfo.Offset, SeekOrigin.Begin); //Seek to file

                            var path = Path.Combine(outputPath, barFilesInfo.RootPath,
                                Path.GetDirectoryName(barFileInfo.FileName) ?? string.Empty);

                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            var filePath = Path.Combine(outputPath, barFilesInfo.RootPath, barFileInfo.FileName);

                            //Extract to tmp file
                            var tempFileName = Path.GetTempFileName();
                            using (var fileStreamFinal =
                                File.Open(tempFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                using (var final = new BinaryWriter(fileStreamFinal))
                                {
                                    var buffer = new byte[4096];
                                    int read;
                                    var totalread = 0L;
                                    while ((read = binReader.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        if (read > barFileInfo.FileSize)
                                        {
                                            totalread = barFileInfo.FileSize;
                                            final.Write(buffer, 0, barFileInfo.FileSize);
                                        }
                                        else if (totalread + read <= barFileInfo.FileSize)
                                        {
                                            totalread += read;
                                            final.Write(buffer, 0, read);
                                        }
                                        else if (totalread + read > barFileInfo.FileSize)
                                        {
                                            var leftToRead = barFileInfo.FileSize - totalread;
                                            totalread = barFileInfo.FileSize;
                                            final.Write(buffer, 0, Convert.ToInt32(leftToRead));
                                        }

                                        if (totalread >= barFileInfo.FileSize)
                                            break;
                                    }
                                }
                            }

                            //Convert file
                            if (convertFile)
                            {
                                if (L33TZipUtils.IsL33TZipFile(tempFileName) &&
                                    !barFileInfo.FileName.EndsWith(".age4scn", StringComparison.OrdinalIgnoreCase))
                                {
                                    var rnd = new Random(Guid.NewGuid().GetHashCode());
                                    var tempFileName2 =
                                        Path.Combine(Path.GetTempPath(),
                                            $"{Path.GetFileName(barFileInfo.FileName)}-{rnd.Next()}.tmp");
                                    L33TZipUtils.ExtractL33TZipFile(tempFileName, tempFileName2);

                                    if (File.Exists(tempFileName))
                                        File.Delete(tempFileName);

                                    tempFileName = tempFileName2;
                                }

                                if (barFileInfo.FileName.EndsWith(".xmb", StringComparison.OrdinalIgnoreCase))
                                {
                                    try
                                    {
                                        var rnd = new Random(Guid.NewGuid().GetHashCode());
                                        var tempFileName2 =
                                            Path.Combine(Path.GetTempPath(),
                                                $"{Path.GetFileName(barFileInfo.FileName)}-{rnd.Next()}.tmp");
                                        XmbFileUtils.XmbToXml(tempFileName, tempFileName2);

                                        if (File.Exists(tempFileName))
                                            File.Delete(tempFileName);

                                        tempFileName = tempFileName2;

                                        filePath = filePath.Substring(0, filePath.Length - 4);
                                    }
                                    catch (Exception)
                                    {
                                        //
                                    }
                                }
                                else if (barFileInfo.FileName.EndsWith(".age4scn",
                                             StringComparison.OrdinalIgnoreCase) &&
                                         !L33TZipUtils.IsL33TZipFile(tempFileName))
                                {
                                    var rnd = new Random(Guid.NewGuid().GetHashCode());
                                    var tempFileName2 =
                                        Path.Combine(Path.GetTempPath(),
                                            $"{Path.GetFileName(barFileInfo.FileName)}-{rnd.Next()}.tmp");
                                    L33TZipUtils.CreateL33TZipFile(tempFileName, tempFileName2);

                                    if (File.Exists(tempFileName))
                                        File.Delete(tempFileName);

                                    tempFileName = tempFileName2;
                                }
                            }

                            //Move new file
                            if (File.Exists(filePath))
                                File.Delete(filePath);

                            //
                            File.Move(tempFileName, filePath);

                            //
                            File.SetCreationTimeUtc(filePath,
                                new DateTime(barFileInfo.LastWriteTime.Year, barFileInfo.LastWriteTime.Month,
                                    barFileInfo.LastWriteTime.Day, barFileInfo.LastWriteTime.Hour,
                                    barFileInfo.LastWriteTime.Minute, barFileInfo.LastWriteTime.Second));

                            File.SetLastWriteTimeUtc(filePath,
                                new DateTime(barFileInfo.LastWriteTime.Year, barFileInfo.LastWriteTime.Month,
                                    barFileInfo.LastWriteTime.Day, barFileInfo.LastWriteTime.Hour,
                                    barFileInfo.LastWriteTime.Minute, barFileInfo.LastWriteTime.Second));
                        }
                    }
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            });
            exceptions.CompleteAdding();
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions.ToArray());
        }

        public static void ExtractBarFile(string inputFile, string file, string outputPath, bool convertFile = true)
        {
            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file), "Value cannot be null or empty.");

            if (!File.Exists(inputFile))
                throw new FileNotFoundException($"File '{inputFile}' not found!", inputFile);

            if (!outputPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                outputPath = outputPath + Path.DirectorySeparatorChar;

            BarFile barFilesInfo;
            using (var fileStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var binReader = new BinaryReader(fileStream))
                {
                    //Read Header
                    binReader.BaseStream.Seek(0, SeekOrigin.Begin); //Seek to header
                    var barFileHeader = new BarFileHeader(binReader);

                    //Read Files Info
                    binReader.BaseStream.Seek(barFileHeader.FilesTableOffset, SeekOrigin.Begin); //Seek to file table

                    barFilesInfo = new BarFile(binReader);
                }
            }

            var barFileInfo = barFilesInfo.BarFileEntrys.First(
                key => string.Equals(key.FileName, file, StringComparison.OrdinalIgnoreCase));

            using (var fileStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var binReader = new BinaryReader(fileStream))
                {
                    binReader.BaseStream.Seek(barFileInfo.Offset, SeekOrigin.Begin); //Seek to file

                    var path = Path.Combine(outputPath, barFilesInfo.RootPath,
                        Path.GetDirectoryName(barFileInfo.FileName) ?? string.Empty);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var filePath = Path.Combine(outputPath, barFilesInfo.RootPath, barFileInfo.FileName);

                    //Extract to tmp file
                    var tempFileName = Path.GetTempFileName();
                    using (var fileStreamFinal =
                        File.Open(tempFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (var final = new BinaryWriter(fileStreamFinal))
                        {
                            var buffer = new byte[4096];
                            int read;
                            var totalread = 0L;
                            while ((read = binReader.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                if (read > barFileInfo.FileSize)
                                {
                                    totalread = barFileInfo.FileSize;
                                    final.Write(buffer, 0, barFileInfo.FileSize);
                                }
                                else if (totalread + read <= barFileInfo.FileSize)
                                {
                                    totalread += read;
                                    final.Write(buffer, 0, read);
                                }
                                else if (totalread + read > barFileInfo.FileSize)
                                {
                                    var leftToRead = barFileInfo.FileSize - totalread;
                                    totalread = barFileInfo.FileSize;
                                    final.Write(buffer, 0, Convert.ToInt32(leftToRead));
                                }

                                if (totalread >= barFileInfo.FileSize)
                                    break;
                            }
                        }
                    }

                    //Convert file
                    if (convertFile)
                    {
                        if (L33TZipUtils.IsL33TZipFile(tempFileName) &&
                            !barFileInfo.FileName.EndsWith(".age4scn", StringComparison.OrdinalIgnoreCase))
                        {
                            var rnd = new Random(Guid.NewGuid().GetHashCode());
                            var tempFileName2 =
                                Path.Combine(Path.GetTempPath(),
                                    $"{Path.GetFileName(barFileInfo.FileName)}-{rnd.Next()}.tmp");
                            L33TZipUtils.ExtractL33TZipFile(tempFileName, tempFileName2);

                            if (File.Exists(tempFileName))
                                File.Delete(tempFileName);

                            tempFileName = tempFileName2;
                        }

                        if (barFileInfo.FileName.EndsWith(".xmb", StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                var rnd = new Random(Guid.NewGuid().GetHashCode());
                                var tempFileName2 =
                                    Path.Combine(Path.GetTempPath(),
                                        $"{Path.GetFileName(barFileInfo.FileName)}-{rnd.Next()}.tmp");
                                XmbFileUtils.XmbToXml(tempFileName, tempFileName2);

                                if (File.Exists(tempFileName))
                                    File.Delete(tempFileName);

                                tempFileName = tempFileName2;

                                filePath = filePath.Substring(0, filePath.Length - 4);
                            }
                            catch (Exception)
                            {
                                //
                            }
                        }
                        else if (barFileInfo.FileName.EndsWith(".age4scn",
                                     StringComparison.OrdinalIgnoreCase) &&
                                 !L33TZipUtils.IsL33TZipFile(tempFileName))
                        {
                            var rnd = new Random(Guid.NewGuid().GetHashCode());
                            var tempFileName2 =
                                Path.Combine(Path.GetTempPath(),
                                    $"{Path.GetFileName(barFileInfo.FileName)}-{rnd.Next()}.tmp");
                            L33TZipUtils.CreateL33TZipFile(tempFileName, tempFileName2);

                            if (File.Exists(tempFileName))
                                File.Delete(tempFileName);

                            tempFileName = tempFileName2;
                        }
                    }

                    //Move new file
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    //
                    File.Move(tempFileName, filePath);

                    //
                    File.SetCreationTimeUtc(filePath,
                        new DateTime(barFileInfo.LastWriteTime.Year, barFileInfo.LastWriteTime.Month,
                            barFileInfo.LastWriteTime.Day, barFileInfo.LastWriteTime.Hour,
                            barFileInfo.LastWriteTime.Minute, barFileInfo.LastWriteTime.Second));

                    File.SetLastWriteTimeUtc(filePath,
                        new DateTime(barFileInfo.LastWriteTime.Year, barFileInfo.LastWriteTime.Month,
                            barFileInfo.LastWriteTime.Day, barFileInfo.LastWriteTime.Hour,
                            barFileInfo.LastWriteTime.Minute, barFileInfo.LastWriteTime.Second));
                }
            }
        }

        public static void CreateBarFile(IReadOnlyCollection<FileInfo> fileInfos, string inputPath,
            string outputFileName, string rootdir, bool ignoreLastWriteTime = true)
        {
            if (inputPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                inputPath = inputPath.Substring(0, inputPath.Length - 1);

            var folder = Path.GetDirectoryName(outputFileName);
            if (folder != null && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var newFilesInfos = new List<FileInfo>();
            foreach (var file in fileInfos.ToArray())
                if (file.FullName.EndsWith(".age4scn", StringComparison.OrdinalIgnoreCase) &&
                    L33TZipUtils.IsL33TZipFile(file.FullName))
                {
                    var data = L33TZipUtils.ExtractL33TZipFile(file.FullName);
                    File.Delete(file.FullName);
                    File.WriteAllBytes(file.FullName, data);
                    newFilesInfos.Add(new FileInfo(file.FullName));
                }
                else
                {
                    newFilesInfos.Add(file);
                }

            using (var fileStream = File.Open(outputFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var writer = new BinaryWriter(fileStream))
                {
                    //Write Bar Header
                    var header = new BarFileHeader(Path.GetFileName(outputFileName), newFilesInfos);
                    writer.Write(header.ToByteArray());

                    //Write Files
                    var barEntrys = new List<BarEntry>();
                    foreach (var file in newFilesInfos)
                    {
                        var filePath = file.FullName;
                        barEntrys.Add(new BarEntry(inputPath, file, (int) writer.BaseStream.Position,
                            ignoreLastWriteTime));
                        using (var fileStream2 = File.Open(filePath, FileMode.Open, FileAccess.Read,
                            FileShare.Read))
                        {
                            using (var binReader = new BinaryReader(fileStream2))
                            {
                                var buffer = new byte[4096];
                                int read;
                                while ((read = binReader.Read(buffer, 0, buffer.Length)) > 0)
                                    writer.Write(buffer, 0, read);
                            }
                        }
                    }

                    //Write Bar Entrys
                    var end = new BarFile(rootdir, barEntrys);
                    writer.Write(end.ToByteArray());
                }
            }
        }

        public static void CreateBarFile(string inputPath, string outputFileName, string rootdir,
            bool ignoreLastWriteTime = true)
        {
            var fileInfos = Directory.GetFiles(inputPath, "*", SearchOption.AllDirectories)
                .Select(fileName => new FileInfo(fileName)).ToArray();

            CreateBarFile(fileInfos, inputPath, outputFileName, rootdir, ignoreLastWriteTime);
        }

        public static void ConvertToNullBarFile(string inputFile, string outputPath, string fileName,
            bool ignoreLastWriteTime = true)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException($"File '{inputFile}' not found!", inputFile);

            string rootName;
            using (var fileStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (var binReader = new BinaryReader(fileStream))
                {
                    //Read Header
                    binReader.BaseStream.Seek(0, SeekOrigin.Begin); //Seek to header
                    var barFileHeader = new BarFileHeader(binReader);

                    //Read Files Info
                    binReader.BaseStream.Seek(barFileHeader.FilesTableOffset, SeekOrigin.Begin); //Seek to file table
                    var barFilesInfo = new BarFile(binReader);

                    rootName = barFilesInfo.RootPath;
                }
            }

            var path = Path.GetRandomFileName();
            var inputPath = Path.Combine(Path.GetTempPath(), path, rootName);
            if (!Directory.Exists(inputPath))
                Directory.CreateDirectory(inputPath);

            inputPath = inputPath.EndsWith(Path.DirectorySeparatorChar.ToString())
                ? inputPath.Substring(0, inputPath.Length - 1)
                : inputPath;

            var nullFile = Path.Combine(inputPath, "null");
            File.WriteAllText(nullFile, string.Empty);

            var outputFile = Path.Combine(outputPath, rootName, fileName);
            CreateBarFile(inputPath, outputFile, rootName, ignoreLastWriteTime);
        }
    }
}