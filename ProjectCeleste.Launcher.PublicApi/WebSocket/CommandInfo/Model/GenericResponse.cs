#region Using directives

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Interface;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model
{
    public class GenericResponse : IGenericResponse
    {
        [JsonConstructor]
        internal GenericResponse([JsonProperty("Result", Required = Required.Always)]
            bool result,
            [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
            string message = null,
            [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
            CommandErrorCode errorCode = CommandErrorCode.None)
        {
            Result = result;
            Message = message;
            ErrorCode = errorCode;
        }

        [Required]
        [JsonProperty("Result", Required = Required.Always)]
        public bool Result { get; }

        [DefaultValue(null)]
        [JsonProperty("Message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; }

        [DefaultValue(CommandErrorCode.None)]
        [JsonProperty("ErrorCode", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CommandErrorCode ErrorCode { get; }
    }
}