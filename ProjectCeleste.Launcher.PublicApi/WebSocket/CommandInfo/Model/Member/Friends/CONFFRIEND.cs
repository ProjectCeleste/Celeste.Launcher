#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Friends
{
    public class ConfirmFriendRequest : GenericRequest
    {
        public const string CommandName = "CONFFRIEND";

        [JsonConstructor]
        public ConfirmFriendRequest([JsonProperty("FriendXuid", Required = Required.Always)]
            long friendXuid)
        {
            FriendXuid = friendXuid;
        }

        [Required]
        [Range(0, long.MaxValue)]
        [JsonProperty("FriendXuid", Required = Required.Always)]
        public long FriendXuid { get; }
    }

    public sealed class ConfirmFriendResponse : GenericResponse
    {
        public static ConfirmFriendResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new ConfirmFriendResponse(false, message, errorCode);
        }

        public static ConfirmFriendResponse FailResponse(string message)
        {
            return new ConfirmFriendResponse(false, message, CommandErrorCode.Unknow);
        }

        public static ConfirmFriendResponse SuccessResponse()
        {
            return new ConfirmFriendResponse(true);
        }

        [JsonConstructor]
        private ConfirmFriendResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}