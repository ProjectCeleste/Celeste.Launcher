#region Using directives

using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface;
using System;
using System.ComponentModel.DataAnnotations;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.CommandInfo.NotLogged
{
    public class ResetPwdInfo
    {
        [JsonConstructor]
        public ResetPwdInfo([JsonProperty("Version", Required = Required.Always)] Version version,
            [JsonProperty("EMail", Required = Required.Always)] string eMail,
            [JsonProperty("VerifyKey", Required = Required.Always)] string verifyKey)
        {
            Version = version;
            EMail = eMail;
            VerifyKey = verifyKey;
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
    }

    public class ResetPwdResult : IGenericResponse
    {
        [JsonConstructor]
        public ResetPwdResult([JsonProperty("Result", Required = Required.Always)] bool result,
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