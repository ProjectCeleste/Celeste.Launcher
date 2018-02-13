#region Using directives

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.NotLogged
{
    public class RegisterUserInfo
    {
        [JsonConstructor]
        public RegisterUserInfo([JsonProperty("Version", Required = Required.Always)] Version version,
            [JsonProperty("Mail", Required = Required.Always)] string mail,
            [JsonProperty("VerifyKey", Required = Required.Always)] string verifyKey,
            [JsonProperty("UserName", Required = Required.Always)] string userName,
            [JsonProperty("Password", Required = Required.Always)] string password,
            [JsonProperty("FingerPrint")] string fingerPrint)
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
        
        [JsonProperty("FingerPrint")]
        public string FingerPrint { get; }
    }

    public class RegisterUserResult
    {
        [JsonConstructor]
        public RegisterUserResult([JsonProperty("Result", Required = Required.Always)] bool result,
            [JsonProperty("Message")] string message = null)
        {
            Result = result;
            Message = message;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [JsonProperty("Message")]
        public string Message { get; }
    }
}