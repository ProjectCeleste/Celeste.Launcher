#region Using directives

using System.Runtime.Serialization;
using System.Xml.Serialization;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.Enum
{
    public enum UserRankEnum : byte
    {
        [XmlEnum("Banned")] [EnumMember(Value = "Banned")]
        Banned = 0,

        [XmlEnum("Member")] [EnumMember(Value = "Member")]
        Member = 1,

        [XmlEnum("Donator")] [EnumMember(Value = "Donator")]
        Donator = 2,

        [XmlEnum("Supporter")] [EnumMember(Value = "Supporter")]
        Supporter = 3,

        [XmlEnum("ChampionSupporter")] [EnumMember(Value = "ChampionSupporter")]
        ChampionSupporter = 4,

        [XmlEnum("GameDev")] [EnumMember(Value = "GameDev")]
        GameDev = 5,

        [XmlEnum("Modo")] [EnumMember(Value = "Modo")]
        Modo = 6,

        [XmlEnum("SuperModo")] [EnumMember(Value = "SuperModo")]
        SuperModo = 7,

        [XmlEnum("Admin")] [EnumMember(Value = "Admin")]
        Admin = 8,

        [XmlEnum("Root")] [EnumMember(Value = "Root")]
        Root = 255
    }
}