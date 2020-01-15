#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Friends
{
    public class RemoveFriendRequest : GenericRequest
    {
        public const string CommandName = "REMFRIEND";

        [JsonConstructor]
        public RemoveFriendRequest([JsonProperty("FriendXuid", Required = Required.Always)]
            long friendXuid)
        {
            FriendXuid = friendXuid;
        }

        [Required]
        [Range(0, long.MaxValue)]
        [JsonProperty("FriendXuid", Required = Required.Always)]
        public long FriendXuid { get; }
    }

    public sealed class RemoveFriendResponse : GenericResponse
    {
        public static RemoveFriendResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new RemoveFriendResponse(false, message, errorCode);
        }

        public static RemoveFriendResponse FailResponse(string message)
        {
            return new RemoveFriendResponse(false, message, CommandErrorCode.Unknow);
        }

        public static RemoveFriendResponse SuccessResponse()
        {
            return new RemoveFriendResponse(true);
        }

        [JsonConstructor]
        private RemoveFriendResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}