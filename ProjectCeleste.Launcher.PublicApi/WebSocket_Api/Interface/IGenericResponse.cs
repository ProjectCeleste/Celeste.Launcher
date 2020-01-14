namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Interface
{
    public interface IGenericResponse
    {
        bool Result { get; }
        string Message { get; }
    }
}