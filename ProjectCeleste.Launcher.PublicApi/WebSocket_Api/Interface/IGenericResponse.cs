namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface
{
    public interface IGenericResponse
    {
        bool Result { get; }
        string Message { get; }
    }
}