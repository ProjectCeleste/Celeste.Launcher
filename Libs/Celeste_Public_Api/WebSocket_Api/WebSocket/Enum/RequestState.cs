namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Enum
{
    public enum RequestState
    {
        TimedOut = -2,
        Failed = -1,
        Idle = 0,
        InProgress = 1,
        Success = 2
    }
}