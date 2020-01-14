#region Using directives

using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Interface;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.CommandInfo.Member
{
    public class FriendJson
    {
        [JsonConstructor]
        public FriendJson([JsonProperty("xuid", Required = Required.Always)] long xuid,
            [JsonProperty("username", Required = Required.Always)] string profileName,
            [JsonProperty("isconnected", Required = Required.Always)] bool isConnected,
            [JsonProperty("richPresence", Required = Required.Always)] string richPresence)
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

    public class FriendsJson
    {
        [JsonConstructor]
        public FriendsJson([JsonProperty("friend-results")] IEnumerable<FriendJson> friends)
        {
            Friends = friends;
        }

        [Required]
        [JsonProperty("friend-results")]
        public IEnumerable<FriendJson> Friends { get; }
    }

    public class GetFriendsInfo
    {
    }

    public class GetFriendsResult : IGenericResponse
    {
        public GetFriendsResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        [JsonConstructor]
        public GetFriendsResult([JsonProperty("Result", Required = Required.Always)] bool result,
            [JsonProperty("Message")] string message, [JsonProperty("Friends")] FriendsJson friends)
        {
            Result = result;
            Message = message;
            Friends = friends;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [JsonProperty("Message")]
        public string Message { get; }

        [JsonProperty("Friends")]
        public FriendsJson Friends { get; }
    }
}