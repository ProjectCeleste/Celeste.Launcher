#region Using directives

using System.ComponentModel;
using System.Xml.Serialization;

#endregion

namespace Celeste_Public_Api.GameScanner_Api.Models
{
    [XmlRoot(ElementName = "FileInfo")]
    public class GameFileInfo
    {
        [XmlAttribute(AttributeName = "FileName")]
        public string FileName { get; set; }

        [DefaultValue(null)]
        [XmlAttribute(AttributeName = "OverrideFileName")]
        public string OverrideFileName { get; set; }

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
}