#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectCeleste.Launcher.PublicApi.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.Model
{
    public class User
    {
        [JsonConstructor]
        public User([JsonProperty("Ip", Required = Required.Always)]
            string ip,
            [JsonProperty("Mail", Required = Required.Always)]
            string mail,
            [JsonProperty("ProfileName", Required = Required.Always)]
            string profileName,
            [JsonProperty("Rank", Required = Required.Always)]
            UserRankEnum rank,
            [JsonProperty("Xuid", Required = Required.Always)]
            long xuid)
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
        public UserRankEnum Rank { get; }
    }
}