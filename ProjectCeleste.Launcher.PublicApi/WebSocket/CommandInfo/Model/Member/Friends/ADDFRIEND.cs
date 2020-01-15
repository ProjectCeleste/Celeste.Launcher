#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Friends
{
    public class AddFriendRequest : GenericRequest
    {
        public const string CommandName = "ADDFRIEND";

        [JsonConstructor]
        public AddFriendRequest([JsonProperty("FriendName", Required = Required.Always)]
            string friendName)
        {
            FriendName = friendName;
        }

        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [MaxLength(16)]
        [JsonProperty("FriendName", Required = Required.Always)]
        public string FriendName { get; }
    }

    public sealed class AddFriendResponse : GenericResponse
    {
        public static AddFriendResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new AddFriendResponse(false, message, errorCode);
        }

        public static AddFriendResponse FailResponse(string message)
        {
            return new AddFriendResponse(false, message, CommandErrorCode.Unknow);
        }

        public static AddFriendResponse SuccessResponse()
        {
            return new AddFriendResponse(true);
        }

        [JsonConstructor]
        private AddFriendResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}