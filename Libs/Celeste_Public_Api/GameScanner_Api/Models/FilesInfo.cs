#region Using directives

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.GameScanner_Api.Models
{
    [XmlRoot(ElementName = "FilesInfo")]
    public class GameFilesInfo
    {
        [XmlIgnore]
        public Dictionary<string, GameFileInfo> FileInfo { get; } = new Dictionary<string, GameFileInfo>();

        [XmlElement(ElementName = "FilesInfo")]
        public GameFileInfo[] FilesInfoArray
        {
            get => FileInfo.Values.ToArray();
            set
            {
                if (value == null) return;
                foreach (var item in value)
                    FileInfo.Add(item.FileName.ToLower(), item);
            }
        }

        public void ToXml(string filename)
        {
            XmlUtils.SerializeToFile(this, filename);
        }
    }
}