using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ProjectCeleste.GameFiles.GameScanner.Models
{
    [XmlRoot(ElementName = "FileInfo")]
    [JsonObject(Title = "GameFileInfo", Description = "")]
    public class GameFileInfo
    {
        public GameFileInfo()
        {
        }

        [JsonConstructor]
        public GameFileInfo([JsonProperty(PropertyName = "FileName", Required = Required.Always)]
            string fileName,
            [JsonProperty(PropertyName = "CRC32", Required = Required.Always)]
            uint crc32,
            [JsonProperty(PropertyName = "Size", Required = Required.Always)]
            long size,
            [JsonProperty(PropertyName = "HttpLink", Required = Required.Always)]
            string httpLink,
            [JsonProperty(PropertyName = "BinCRC32", Required = Required.Always)]
            uint binCrc32,
            [JsonProperty(PropertyName = "BinSize", Required = Required.Always)]
            long binSize)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Crc32 = crc32;
            Size = size;
            HttpLink = httpLink ?? throw new ArgumentNullException(nameof(httpLink));
            BinCrc32 = binCrc32;
            BinSize = binSize;
        }

        [Key]
        [Required(AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "FileName", Required = Required.Always)]
        [XmlAttribute(AttributeName = "FileName")]
        public string FileName { get; set; }

        [Required]
        [Range(0, uint.MaxValue)]
        [JsonProperty(PropertyName = "CRC32", Required = Required.Always)]
        [XmlAttribute(AttributeName = "CRC32")]
        public uint Crc32 { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        [JsonProperty(PropertyName = "Size", Required = Required.Always)]
        [XmlAttribute(AttributeName = "Size")]
        public long Size { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "HttpLink", Required = Required.Always)]
        [XmlAttribute(AttributeName = "HttpLink")]
        public string HttpLink { get; set; }

        [Required]
        [Range(0, uint.MaxValue)]
        [JsonProperty(PropertyName = "BinCRC32", Required = Required.Always)]
        [XmlAttribute(AttributeName = "BinCRC32")]
        public uint BinCrc32 { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        [JsonProperty(PropertyName = "BinSize", Required = Required.Always)]
        [XmlAttribute(AttributeName = "BinSize")]
        public long BinSize { get; set; }
    }

    [JsonObject(Title = "GameFilesInfo", Description = "")]
    [XmlRoot(ElementName = "FilesInfo")]
    public class GameFilesInfo
    {
        public GameFilesInfo()
        {
            Version = new Version(4, 0, 0, 6148);
            GameFileInfo = new Dictionary<string, GameFileInfo>(StringComparer.OrdinalIgnoreCase);
        }

        [JsonConstructor]
        public GameFilesInfo([JsonProperty(PropertyName = "Version", Required = Required.Always)]
            Version version,
            [JsonProperty(PropertyName = "GameFileInfo", Required = Required.Always)]
            IEnumerable<GameFileInfo> gameFileInfo)
        {
            Version = version;
            GameFileInfo = (gameFileInfo as GameFileInfo[] ?? gameFileInfo.ToArray()).ToDictionary(key => key.FileName,
                StringComparer.OrdinalIgnoreCase);
        }

        [Required]
        [JsonProperty(PropertyName = "Version", Required = Required.Always)]
        [XmlIgnore]
        public Version Version { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonIgnore]
        [XmlAttribute(AttributeName = "Version")]
        public string VersionString
        {
            get => Version.ToString();
            set => Version = new Version(value);
        }

        [JsonIgnore] [XmlIgnore] public IDictionary<string, GameFileInfo> GameFileInfo { get; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Required]
        [JsonProperty(PropertyName = "GameFileInfo", Required = Required.Always)]
        [XmlElement(ElementName = "FilesInfo")]
        public GameFileInfo[] GameFileInfoArray
        {
            get => GameFileInfo.Values.ToArray();
            set
            {
                GameFileInfo.Clear();
                if (value == null)
                    return;
                foreach (var item in value)
                    GameFileInfo.Add(item.FileName, item);
            }
        }
    }
}