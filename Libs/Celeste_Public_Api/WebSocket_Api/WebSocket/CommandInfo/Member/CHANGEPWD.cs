#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member
{
    public class ChangePwdInfo
    {
        [JsonConstructor]
        public ChangePwdInfo([JsonProperty("Old", Required = Required.Always)] string oldpass,
            [JsonProperty("New", Required = Required.Always)] string newpass)
        {
            Old = oldpass;
            New = newpass;
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

    public class ChangePwdResult : IGenericResponse
    {
        [JsonConstructor]
        public ChangePwdResult([JsonProperty("Result", Required = Required.Always)] bool result,
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