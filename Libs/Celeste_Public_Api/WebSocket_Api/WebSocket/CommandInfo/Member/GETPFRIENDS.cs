#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member
{
    public class GetPendingFriendsInfo
    {
    }

    public class GetPendingFriendsResult
    {
        public GetPendingFriendsResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }

        [JsonConstructor]
        public GetPendingFriendsResult([JsonProperty("Result", Required = Required.Always)] bool result,
            [JsonProperty("Message")] string message,
            [JsonProperty("PendingFriendsRequest")] FriendsJson pendingFriendsRequest,
            [JsonProperty("PendingFriendsInvite")] FriendsJson pendingFriendsInvite)
        {
            Result = result;
            Message = message;
            PendingFriendsRequest = pendingFriendsRequest;
            PendingFriendsInvite = pendingFriendsInvite;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [JsonProperty("Message")]
        public string Message { get; }

        [JsonProperty("PendingFriendsRequest")]
        public FriendsJson PendingFriendsRequest { get; }

        [JsonProperty("PendingFriendsInvite")]
        public FriendsJson PendingFriendsInvite { get; }
    }
}