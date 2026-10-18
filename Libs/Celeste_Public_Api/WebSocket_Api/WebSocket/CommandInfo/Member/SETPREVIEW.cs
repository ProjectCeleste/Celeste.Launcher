#region Using directives

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member
{
    public class SetPreviewInfo
    {
        [JsonConstructor]
        public SetPreviewInfo([JsonProperty("IsPreview", Required = Required.Always)] bool isPreview)
        {
            IsPreview = isPreview;
        }

        [Required]
        [JsonProperty("IsPreview", Required = Required.Always)]
        public bool IsPreview { get; }
    }

    public class SetPreviewResult : IGenericResponse
    {
        [JsonConstructor]
        public SetPreviewResult([JsonProperty("Result", Required = Required.Always)] bool result,
            [JsonProperty("Message")] string message = null,
            [JsonProperty("IsPreview")] bool isPreview = false)
        {
            Result = result;
            Message = message;
            IsPreview = isPreview;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [JsonProperty("Message")]
        public string Message { get; }

        [JsonProperty("IsPreview")]
        public bool IsPreview { get; }
    }
}
