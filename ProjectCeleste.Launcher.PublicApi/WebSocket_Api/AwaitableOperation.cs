using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket
{
    internal class AwaitableOperation<TResponse> where TResponse : IGenericResponse
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0, 1);

        private TResponse _response;

        public void ReceivedMessage(TResponse response)
        {
            _response = response;
            _semaphore.Release();
        }

        public async Task<TResponse> WaitForResponseAsync(int timeoutInSeconds)
        {
            bool waitSucceeded = await _semaphore.WaitAsync(timeoutInSeconds * 1000);

            if (!waitSucceeded)
                throw new System.Exception("Operation timed out");

            return _response;
        }
    }
}