#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Password
{
    public class ChangePwdRequest : GenericRequest
    {
        public const string CommandName = "CHANGEPWD";

        [JsonConstructor]
        public ChangePwdRequest([JsonProperty("Old", Required = Required.Always)]
            string oldPass,
            [JsonProperty("New", Required = Required.Always)]
            string newPass)
        {
            Old = oldPass;
            New = newPass;
        }

        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [MaxLength(255)]
        [JsonProperty("Old", Required = Required.Always)]
        public string Old { get; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        [MaxLength(255)]
        [JsonProperty("New", Required = Required.Always)]
        public string New { get; }
    }

    public sealed class ChangePwdResponse : GenericResponse
    {
        public static ChangePwdResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new ChangePwdResponse(false, message, errorCode);
        }

        public static ChangePwdResponse FailResponse(string message)
        {
            return new ChangePwdResponse(false, message, CommandErrorCode.Unknow);
        }

        public static ChangePwdResponse SuccessResponse()
        {
            return new ChangePwdResponse(true);
        }

        [JsonConstructor]
        private ChangePwdResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}