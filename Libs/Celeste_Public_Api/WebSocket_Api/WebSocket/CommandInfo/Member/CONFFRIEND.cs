#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member
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