#region Using directives

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.Model;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest
{
    public class LoginInfo : GenericRequest
    {
        public const string CommandName = "LOGIN";

        [JsonConstructor]
        public LoginInfo([JsonProperty("Mail", Required = Required.Always)]
            string mail,
            [JsonProperty("Password", Required = Required.Always)]
            string password,
            [JsonProperty("Version", Required = Required.Always)]
            Version version,
            [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string fingerPrint = null)
        {
            Mail = mail;
            Password = password;
            Version = version;
            FingerPrint = fingerPrint;
        }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Mail", Required = Required.Always)]
        public string Mail { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("Password", Required = Required.Always)]
        public string Password { get; }

        [Required]
        [JsonProperty("Version", Required = Required.Always)]
        public Version Version { get; }

        [DefaultValue(null)]
        [JsonProperty("FingerPrint", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FingerPrint { get; }
    }

    public sealed class LoginResponse : GenericResponse
    {
        public static LoginResponse FailResponse(CommandErrorCode errorCode = CommandErrorCode.Unknow,
            string message = null)
        {
            return new LoginResponse(false, message, errorCode);
        }

        public static LoginResponse FailResponse(string message)
        {
            return new LoginResponse(false, message, CommandErrorCode.Unknow);
        }

        public static LoginResponse SuccessResponse(User user)
        {
            return new LoginResponse(true, user: user ?? throw new ArgumentNullException(nameof(user)));
        }

        [JsonConstructor]
        private LoginResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None,
            [JsonProperty("User", DefaultValueHandling = DefaultValueHandling.Ignore)]
            User user = null) : base(result, message, errorCode)
        {
            User = user;
        }

        [DefaultValue(null)]
        [JsonProperty("User", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public User User { get; }
    }
}