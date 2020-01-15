#region Using directives

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest.Password
{
    public class ForgotPwdRequest : GenericRequest
    {
        public const string CommandName = "FORGOTPWD";

        [JsonConstructor]
        public ForgotPwdRequest([JsonProperty("Version", Required = Required.Always)]
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

    public sealed class ForgotPwdResponse : GenericResponse
    {
        public static ForgotPwdResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new ForgotPwdResponse(false, message, errorCode);
        }

        public static ForgotPwdResponse FailResponse(string message)
        {
            return new ForgotPwdResponse(false, message, CommandErrorCode.Unknow);
        }

        public static ForgotPwdResponse SuccessResponse()
        {
            return new ForgotPwdResponse(true);
        }

        [JsonConstructor]
        private ForgotPwdResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}