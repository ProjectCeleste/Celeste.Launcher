#region Using directives

using System.Xml.Serialization;

#endregion

namespace Celeste_User.Enum
{
    public enum Rank
    {
        [XmlEnum("Banned")] Banned = 0,
        [XmlEnum("Member")] Member = 1,
        [XmlEnum("Donator")] Donator = 2,
        [XmlEnum("ChampionSupporter")] ChampionSupporter = 3,
        [XmlEnum("Modo")] Modo = 4,
        [XmlEnum("SuperModo")] SuperModo = 5,
        [XmlEnum("Admin")] Admin = 6
    }
}