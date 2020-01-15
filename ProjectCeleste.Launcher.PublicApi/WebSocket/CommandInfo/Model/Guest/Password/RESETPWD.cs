#region Using directives

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest.Password
{
    public class ResetPwdRequest : GenericRequest
    {
        public const string CommandName = "RESETPWD";

        [JsonConstructor]
        public ResetPwdRequest([JsonProperty("Version", Required = Required.Always)]
            Version version,
            [JsonProperty("EMail", Required = Required.Always)]
            string eMail,
            [JsonProperty("VerifyKey", Required = Required.Always)]
            string verifyKey,
            [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string fingerPrint = null)
        {
            Version = version;
            EMail = eMail;
            VerifyKey = verifyKey;
            FingerPrint = fingerPrint;
        }

        [Required]
        [JsonProperty("Version", Required = Required.Always)]
        public Version Version { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("EMail", Required = Required.Always)]
        public string EMail { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("VerifyKey", Required = Required.Always)]
        public string VerifyKey { get; }

        [DefaultValue(null)]
        [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FingerPrint { get; }
    }

    public sealed class ResetPwdResponse : GenericResponse
    {
        public static ResetPwdResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new ResetPwdResponse(false, message, errorCode);
        }

        public static ResetPwdResponse FailResponse(string message)
        {
            return new ResetPwdResponse(false, message, CommandErrorCode.Unknow);
        }

        public static ResetPwdResponse SuccessResponse()
        {
            return new ResetPwdResponse(true);
        }

        [JsonConstructor]
        private ResetPwdResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}