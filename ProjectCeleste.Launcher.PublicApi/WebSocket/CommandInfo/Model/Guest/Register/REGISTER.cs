#region Using directives

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest.Register
{
    public class RegisterUserRequest : GenericRequest
    {
        public const string CommandName = "REGISTER";

        [JsonConstructor]
        public RegisterUserRequest([JsonProperty("Version", Required = Required.Always)]
            Version version,
            [JsonProperty("Mail", Required = Required.Always)]
            string mail,
            [JsonProperty("VerifyKey", Required = Required.Always)]
            string verifyKey,
            [JsonProperty("UserName", Required = Required.Always)]
            string userName,
            [JsonProperty("Password", Required = Required.Always)]
            string password,
            [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string fingerPrint = null)
        {
            Version = version;
            Mail = mail;
            VerifyKey = verifyKey;
            UserName = userName;
            Password = password;
            FingerPrint = fingerPrint;
        }

        [Required]
        [JsonProperty("Version", Required = Required.Always)]
        public Version Version { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Mail", Required = Required.Always)]
        public string Mail { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("VerifyKey", Required = Required.Always)]
        public string VerifyKey { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("UserName", Required = Required.Always)]
        public string UserName { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Password", Required = Required.Always)]
        public string Password { get; }

        [DefaultValue(null)]
        [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FingerPrint { get; }
    }

    public sealed class RegisterUserResponse : GenericResponse
    {
        public static RegisterUserResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new RegisterUserResponse(false, message, errorCode);
        }

        public static RegisterUserResponse FailResponse(string message)
        {
            return new RegisterUserResponse(false, message, CommandErrorCode.Unknow);
        }

        public static RegisterUserResponse SuccessResponse()
        {
            return new RegisterUserResponse(true);
        }

        [JsonConstructor]
        private RegisterUserResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None) : base(result, message, errorCode)
        {
        }
    }
}