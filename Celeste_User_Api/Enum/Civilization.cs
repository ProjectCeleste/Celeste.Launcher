#region Using directives

using System.Xml.Serialization;

#endregion

namespace Celeste_User.Enum
{
    public enum Civilization
    {
        [XmlEnum("Any")] Any = 0,
        [XmlEnum("Greek")] Greek = 1,
        [XmlEnum("Egypt")] Egypt = 3,
        [XmlEnum("Celt")] Celt = 6,
        [XmlEnum("Persia")] Persia = 8,
        [XmlEnum("Babylonian")] Babylonian = 22,
        [XmlEnum("Norse")] Norse = 24
    }
}