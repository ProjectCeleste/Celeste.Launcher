#region Using directives

using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Interface;
using System.ComponentModel.DataAnnotations;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.CommandInfo.Member
{
    public class GetPendingFriendsInfo
    {
    }

    public class GetPendingFriendsResult : IGenericResponse
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