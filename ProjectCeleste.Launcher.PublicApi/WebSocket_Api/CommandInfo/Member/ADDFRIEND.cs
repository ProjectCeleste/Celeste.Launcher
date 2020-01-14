#region Using directives

using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface;
using System.ComponentModel.DataAnnotations;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.Member
{
    public class AddFriendInfo
    {
        [JsonConstructor]
        public AddFriendInfo([JsonProperty("FriendName", Required = Required.Always)] string friendName)
        {
            FriendName = friendName;
        }

        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [MaxLength(16)]
        [JsonProperty("FriendName", Required = Required.Always)]
        public string FriendName { get; }
    }

    public class AddFriendResult : IGenericResponse
    {
        [JsonConstructor]
        public AddFriendResult([JsonProperty("Result", Required = Required.Always)] bool result,
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