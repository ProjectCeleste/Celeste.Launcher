#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

#endregion

namespace Celeste_Public_Api.GameScanner_Api.Models
{
    [XmlRoot(ElementName = "FileInfo")]
    public class GameFileInfo
    {
        [XmlAttribute(AttributeName = "FileName")]
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "CRC32")]
        public uint Crc32 { get; set; }

        [XmlAttribute(AttributeName = "Size")]
        public long Size { get; set; }

        [XmlAttribute(AttributeName = "HttpLink")]
        public string HttpLink { get; set; }

        [XmlAttribute(AttributeName = "BinCRC32")]
        public uint BinCrc32 { get; set; }

        [XmlAttribute(AttributeName = "BinSize")]
        public long BinSize { get; set; }
    }

    [XmlRoot(ElementName = "FilesInfo")]
    public class GameFilesInfo
    {
        [XmlIgnore]
        public Dictionary<string, GameFileInfo> FileInfo { get; } =
            new Dictionary<string, GameFileInfo>(StringComparer.OrdinalIgnoreCase);

        [XmlElement(ElementName = "FilesInfo")]
        public GameFileInfo[] FilesInfoArray
        {
            get => FileInfo.Values.ToArray();
            set
            {
                FileInfo.Clear();
                if (value == null) return;
                foreach (var item in value)
                    FileInfo.Add(item.FileName, item);
            }
        }
    }
}