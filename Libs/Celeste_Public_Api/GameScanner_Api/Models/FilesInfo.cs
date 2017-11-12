#region Using directives

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Public_Api.GameScanner_Api.Models
{
    [XmlRoot(ElementName = "FilesInfo")]
    public class FilesInfo
    {
        [XmlIgnore]
        public Dictionary<string, FileInfo> FileInfo { get; set; } = new Dictionary<string, FileInfo>();

        [XmlElement(ElementName = "FilesInfo")]
        public FileInfo[] FilesInfoArray
        {
            get
            {
                var list = new List<FileInfo>();
                if (FileInfo != null)
                    list.AddRange(FileInfo.Keys.Select(key => FileInfo[key]));

                return list.ToArray();
            }
            set
            {
                FileInfo = new Dictionary<string, FileInfo>();
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