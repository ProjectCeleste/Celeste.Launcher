#region Using directives

using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;

#endregion

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Interface
{
    public interface IGenericResponse
    {
        bool Result { get; }
        string Message { get; }
        CommandErrorCode ErrorCode { get; }
    }
}