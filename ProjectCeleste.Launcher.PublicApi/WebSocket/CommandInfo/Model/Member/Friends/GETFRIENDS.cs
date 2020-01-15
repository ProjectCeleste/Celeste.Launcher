#region Using directives

using System;
using System.ComponentModel;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Friends
{
    public class GetFriendsRequest : GenericRequest
    {
        public const string CommandName = "GETFRIENDS";
    }

    public sealed class GetFriendsResponse : GenericResponse
    {
        public static GetFriendsResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new GetFriendsResponse(false, message, errorCode);
        }

        public static GetFriendsResponse FailResponse(string message)
        {
            return new GetFriendsResponse(false, message, CommandErrorCode.Unknow);
        }

        public static GetFriendsResponse SuccessResponse(PublicApi.Model.Friends friends)
        {
            return new GetFriendsResponse(true, friends: friends ?? throw new ArgumentNullException(nameof(friends)));
        }

        [JsonConstructor]
        private GetFriendsResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None,
            [JsonProperty("Friends", DefaultValueHandling = DefaultValueHandling.Ignore)]
            PublicApi.Model.Friends friends = null) : base(result, message, errorCode)
        {
            Friends = friends;
        }

        [DefaultValue(null)]
        [JsonProperty("Friends", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PublicApi.Model.Friends Friends { get; }
    }
}