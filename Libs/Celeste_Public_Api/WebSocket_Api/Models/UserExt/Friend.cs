#region Using directives

using System.Xml.Serialization;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.Models.UserExt
{
    [XmlRoot(ElementName = "Friend")]
    public class Friend
    {
        [XmlElement(ElementName = "ProfileName")]
        public string ProfileName { get; set; }

        [XmlElement(ElementName = "Xuid")]
        public long Xuid { get; set; }

        [XmlIgnore]
        public bool IsConnected { get; set; }
    }
}