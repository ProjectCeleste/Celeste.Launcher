#region Using directives

using System;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Friends
{
    public class GetPendingFriendsRequest : GenericRequest
    {
        public const string CommandName = "GETPFRIENDS";
    }

    public sealed class GetPendingFriendsResponse : GenericResponse
    {
        public static GetPendingFriendsResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new GetPendingFriendsResponse(false, message, errorCode);
        }

        public static GetPendingFriendsResponse FailResponse(string message)
        {
            return new GetPendingFriendsResponse(false, message, CommandErrorCode.Unknow);
        }

        public static GetPendingFriendsResponse SuccessResponse(PublicApi.Model.Friends pendingFriendsRequest, PublicApi.Model.Friends pendingFriendsInvite)
        {
            return new GetPendingFriendsResponse(true,
                pendingFriendsRequest: pendingFriendsRequest ?? throw new ArgumentNullException(nameof(pendingFriendsRequest)),
                pendingFriendsInvite: pendingFriendsInvite ?? throw new ArgumentNullException(nameof(pendingFriendsInvite)));
        }

        [JsonConstructor]
        private GetPendingFriendsResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None,
            [JsonProperty("PendingFriendsRequest", DefaultValueHandling = DefaultValueHandling.Ignore)]
            PublicApi.Model.Friends pendingFriendsRequest = null,
            [JsonProperty("PendingFriendsInvite", DefaultValueHandling = DefaultValueHandling.Ignore)]
            PublicApi.Model.Friends pendingFriendsInvite = null) : base(result, message, errorCode)
        {
            PendingFriendsRequest = pendingFriendsRequest;
            PendingFriendsInvite = pendingFriendsInvite;
        }

        [JsonProperty("PendingFriendsRequest")]
        public PublicApi.Model.Friends PendingFriendsRequest { get; }

        [JsonProperty("PendingFriendsInvite")] public PublicApi.Model.Friends PendingFriendsInvite { get; }
    }
}