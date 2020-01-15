#region Using directives

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.Model
{
    public class Friends
    {
        [JsonConstructor]
        public Friends([JsonProperty("friend-results")] IEnumerable<Friend> friends)
        {
            Friend = friends;
        }

        [Required]
        [JsonProperty("friend-results")]
        public IEnumerable<Friend> Friend { get; }
    }

    public class Friend
    {
        [JsonConstructor]
        public Friend([JsonProperty("xuid", Required = Required.Always)]
            long xuid,
            [JsonProperty("username", Required = Required.Always)]
            string profileName,
            [JsonProperty("isconnected", Required = Required.Always)]
            bool isConnected,
            [JsonProperty("richPresence", Required = Required.Always)]
            string richPresence)
        {
            Xuid = xuid;
            ProfileName = profileName;
            IsConnected = isConnected;
            RichPresence = richPresence;
        }

        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [MaxLength(15)]
        [JsonProperty("username", Required = Required.Always)]
        public string ProfileName { get; }

        [Key]
        [Required]
        [JsonProperty("xuid", Required = Required.Always)]
        public long Xuid { get; }

        [JsonProperty("isconnected", Required = Required.Always)]
        public bool IsConnected { get; }

        [Required]
        [JsonProperty("richPresence", Required = Required.Always)]
        public string RichPresence { get; }
    }
}