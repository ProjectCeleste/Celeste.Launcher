#region Using directives

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.Member
{
    public class LoginInfo
    {
        [JsonConstructor]
        public LoginInfo([JsonProperty("Mail", Required = Required.Always)] string mail,
            [JsonProperty("Password", Required = Required.Always)] string password,
            [JsonProperty("Version", Required = Required.Always)] Version version,
            [JsonProperty("FingerPrint")] string fingerPrint)
        {
            Mail = mail;
            Password = password;
            Version = version;
            FingerPrint = fingerPrint;
        }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Mail", Required = Required.Always)]
        public string Mail { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Password", Required = Required.Always)]
        public string Password { get; }

        [Required]
        [JsonProperty("Version", Required = Required.Always)]
        public Version Version { get; }

        [JsonProperty("FingerPrint")]
        public string FingerPrint { get; }
    }

    public enum RankEnum
    {
        [XmlEnum("Banned")] [EnumMember(Value = "Banned")] Banned = 0,
        [XmlEnum("Member")] [EnumMember(Value = "Member")] Member = 1,
        [XmlEnum("Donator")] [EnumMember(Value = "Donator")] Donator = 2,
        [XmlEnum("Supporter")] [EnumMember(Value = "Supporter")] Supporter = 3,
        [XmlEnum("ChampionSupporter")] [EnumMember(Value = "ChampionSupporter")] ChampionSupporter = 4,
        [XmlEnum("GameDev")] [EnumMember(Value = "GameDev")] GameDev = 5,
        [XmlEnum("Modo")] [EnumMember(Value = "Modo")] Modo = 6,
        [XmlEnum("SuperModo")] [EnumMember(Value = "SuperModo")] SuperModo = 7,
        [XmlEnum("Admin")] [EnumMember(Value = "Admin")] Admin = 8
    }

    public class User
    {
        [JsonConstructor]
        public User([JsonProperty("Ip", Required = Required.Always)] string ip,
            [JsonProperty("Mail", Required = Required.Always)] string mail,
            [JsonProperty("ProfileName", Required = Required.Always)] string profileName,
            [JsonProperty("Rank", Required = Required.Always)] RankEnum rank,
            [JsonProperty("Xuid", Required = Required.Always)] long xuid)
        {
            Ip = ip;
            Mail = mail;
            ProfileName = profileName;
            Xuid = xuid;
            Rank = rank;
        }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Mail", Required = Required.Always)]
        public string Mail { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("ProfileName", Required = Required.Always)]
        public string ProfileName { get; }

        [Key]
        [Required(AllowEmptyStrings = false)]
        [Range(0, long.MaxValue)]
        [JsonProperty("Xuid", Required = Required.Always)]
        public long Xuid { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Ip", Required = Required.Always)]
        public string Ip { get; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("Rank", Required = Required.Always)]
        public RankEnum Rank { get; }
    }

    public class LoginResult : IGenericResponse
    {
        public LoginResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        [JsonConstructor]
        public LoginResult([JsonProperty("Result", Required = Required.Always)] bool result,
            [JsonProperty("Message")] string message, [JsonProperty("User")] User user)
        {
            Result = result;
            Message = message;
            User = user;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [JsonProperty("Message")]
        public string Message { get; }

        [JsonProperty("User")]
        public User User { get; }
    }
}