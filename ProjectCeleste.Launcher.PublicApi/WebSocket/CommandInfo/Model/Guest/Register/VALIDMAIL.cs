#region Using directives

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest.Register
{
    public class ValidMailRequest : GenericRequest
    {
        public const string CommandName = "VALIDMAIL";

        [JsonConstructor]
        public ValidMailRequest([JsonProperty("Version", Required = Required.Always)]
            Version version,
            [JsonProperty("EMail", Required = Required.Always)]
            string eMail,
            [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string fingerPrint = null)
        {
            Version = version;
            EMail = eMail;
            FingerPrint = fingerPrint;
        }

        [Required]
        [JsonProperty("Version", Required = Required.Always)]
        public Version Version { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("EMail", Required = Required.Always)]
        public string EMail { get; }

        [DefaultValue(null)]
        [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FingerPrint { get; }
    }

    public sealed class ValidMailResponse : GenericResponse
    {
        public static ValidMailResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new ValidMailResponse(false, message, errorCode);
        }

        public static ValidMailResponse FailResponse(string message)
        {
            return new ValidMailResponse(false, message, CommandErrorCode.Unknow);
        }

        public static ValidMailResponse SuccessResponse()
        {
            return new ValidMailResponse(true);
        }

        [JsonConstructor]
        private ValidMailResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}