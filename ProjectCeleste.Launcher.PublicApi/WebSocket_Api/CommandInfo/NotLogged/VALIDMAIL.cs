#region Using directives

using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Interface;
using System;
using System.ComponentModel.DataAnnotations;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.CommandInfo.NotLogged
{
    public class ValidMailInfo
    {
        [JsonConstructor]
        public ValidMailInfo([JsonProperty("Version", Required = Required.Always)] Version version,
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

    public class ValidMailResult : IGenericResponse
    {
        [JsonConstructor]
        public ValidMailResult([JsonProperty("Result", Required = Required.Always)] bool result,
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