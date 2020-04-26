#region Using directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Bar
{
    public class BarEntryLastWriteTime
    {
        public BarEntryLastWriteTime(BinaryReader binaryReader)
        {
            Year = binaryReader.ReadInt16();
            Month = binaryReader.ReadInt16();
            DayOfWeek = binaryReader.ReadInt16();
            Day = binaryReader.ReadInt16();
            Hour = binaryReader.ReadInt16();
            Minute = binaryReader.ReadInt16();
            Second = binaryReader.ReadInt16();
            Msecond = binaryReader.ReadInt16();
        }

        public BarEntryLastWriteTime(DateTime dateTime)
        {
            Year = (short) dateTime.Year;
            Month = (short) dateTime.Month;
            DayOfWeek = (short) dateTime.DayOfWeek;
            Day = (short) dateTime.Day;
            Hour = (short) dateTime.Hour;
            Minute = (short) dateTime.Minute;
            Second = (short) dateTime.Second;
            Msecond = (short) dateTime.Millisecond;
        }

        public short Hour { get; }
        public short Minute { get; }
        public short Second { get; }
        public short Msecond { get; }
        public short Year { get; }
        public short Month { get; }
        public short Day { get; }
        public short DayOfWeek { get; }

        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Year);
                    bw.Write(Month);
                    bw.Write(DayOfWeek);
                    bw.Write(Day);
                    bw.Write(Hour);
                    bw.Write(Minute);
                    bw.Write(Second);
                    bw.Write(Msecond);
                    return ms.ToArray();
                }
            }
        }
    }

    public class BarEntry
    {
        public BarEntry(BinaryReader binaryReader)
        {
            Offset = binaryReader.ReadInt32();
            FileSize = binaryReader.ReadInt32();
            FileSize2 = binaryReader.ReadInt32();
            LastWriteTime = new BarEntryLastWriteTime(binaryReader);
            var length = binaryReader.ReadUInt32();
            FileName = Encoding.Unicode.GetString(binaryReader.ReadBytes((int) length * 2));
        }

        public BarEntry(string filename, int offset, int filesize, BarEntryLastWriteTime modifiedDates)
        {
            FileName = filename;
            Offset = offset;
            FileSize = filesize;
            FileSize2 = filesize;
            LastWriteTime = modifiedDates;
        }

        public BarEntry(string rootPath, FileInfo fileInfo, int offset, bool ignoreLastWriteTime = true)
        {
            rootPath = rootPath.EndsWith(Path.DirectorySeparatorChar.ToString())
                ? rootPath
                : rootPath + Path.DirectorySeparatorChar;

            FileName = fileInfo.FullName.Replace(rootPath, string.Empty);
            Offset = offset;
            FileSize = (int) fileInfo.Length;
            FileSize2 = FileSize;
            LastWriteTime = ignoreLastWriteTime
                ? new BarEntryLastWriteTime(new DateTime(2011, 1, 1))
                : new BarEntryLastWriteTime(fileInfo.LastWriteTimeUtc);
        }

        public string FileName { get; }

        public int Offset { get; }

        public int FileSize { get; }

        public int FileSize2 { get; }

        public BarEntryLastWriteTime LastWriteTime { get; }

        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Offset);
                    bw.Write(FileSize);
                    bw.Write(FileSize2);
                    bw.Write(LastWriteTime.ToByteArray());
                    bw.Write(FileName.Length);
                    bw.Write(Encoding.Unicode.GetBytes(FileName));
                    return ms.ToArray();
                }
            }
        }
    }

    public class BarFile
    {
        public BarFile(string rootPath, IEnumerable<BarEntry> barEntrys)
        {
            var enumerable = barEntrys as BarEntry[] ?? barEntrys.ToArray();
            RootPath = rootPath;
            NumberOfRootFiles = (uint) enumerable.Length;
            BarFileEntrys = enumerable.ToList();
        }

        public BarFile(BinaryReader binaryReader)
        {
            var rootNameLength = binaryReader.ReadUInt32();
            RootPath = Encoding.Unicode.GetString(binaryReader.ReadBytes((int) rootNameLength * 2));
            NumberOfRootFiles = binaryReader.ReadUInt32();
            var barFileEntrys = new List<BarEntry>();
            for (uint i = 0; i < NumberOfRootFiles; i++)
                barFileEntrys.Add(new BarEntry(binaryReader));
            BarFileEntrys = new ReadOnlyCollection<BarEntry>(barFileEntrys);
        }

        public string RootPath { get; }

        public uint NumberOfRootFiles { get; }

        public IReadOnlyCollection<BarEntry> BarFileEntrys { get; }

        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(RootPath.Length);
                    bw.Write(Encoding.Unicode.GetBytes(RootPath));
                    bw.Write(NumberOfRootFiles);
                    foreach (var barFileEntry in BarFileEntrys)
                        bw.Write(barFileEntry.ToByteArray());
                    return ms.ToArray();
                }
            }
        }
    }
}