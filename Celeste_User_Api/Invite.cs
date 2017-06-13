#region Using directives

using System.Xml.Serialization;

#endregion

namespace Celeste_User
{
    public class Invite
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "used")]
        public bool Used { get; set; }

        [XmlElement(ElementName = "usedby")]
        public string UsedByUser { get; set; }

        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
    }
}