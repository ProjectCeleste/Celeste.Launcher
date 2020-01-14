#region Using directives

using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface;
using System.ComponentModel.DataAnnotations;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.Member
{
    public class ConfirmFriendInfo
    {
        [JsonConstructor]
        public ConfirmFriendInfo([JsonProperty("FriendXuid", Required = Required.Always)] long friendXuid)
        {
            FriendXuid = friendXuid;
        }

        [Required]
        [Range(0, long.MaxValue)]
        [JsonProperty("FriendXuid", Required = Required.Always)]
        public long FriendXuid { get; }
    }

    public class ConfirmFriendResult : IGenericResponse
    {
        [JsonConstructor]
        public ConfirmFriendResult([JsonProperty("Result", Required = Required.Always)] bool result,
            [JsonProperty("Message")] string message = null)
        {
            Result = result;
            Message = message;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [JsonProperty("Message")]
        public string Message { get; }
    }
}