#region Using directives

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.NotLogged
{
    public class ForgotPwdInfo
    {
        [JsonConstructor]
        public ForgotPwdInfo([JsonProperty("Version", Required = Required.Always)] Version version,
            [JsonProperty("EMail", Required = Required.Always)] string eMail)
        {
            Version = version;
            EMail = eMail;
        }

        [Required]
        [JsonProperty("Version", Required = Required.Always)]
        public Version Version { get; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("EMail", Required = Required.Always)]
        public string EMail { get; }
    }

    public class ForgotPwdResult
    {
        [JsonConstructor]
        public ForgotPwdResult([JsonProperty("Result", Required = Required.Always)] bool result,
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