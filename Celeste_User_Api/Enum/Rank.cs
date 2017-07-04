#region Using directives

using System.Xml.Serialization;

#endregion

namespace Celeste_User.Enum
{
    public enum Rank
    {
        [XmlEnum("Member")] Member = 0,
        [XmlEnum("Donator")] Donator = 1,
        [XmlEnum("ChampionSupporter")] ChampionSupporter = 2,
        [XmlEnum("Modo")] Modo = 3,
        [XmlEnum("SuperModo")] SuperModo = 4,
        [XmlEnum("Admin")] Admin = 5,
        [XmlEnum("Banned")] Banned = 6
    }
}