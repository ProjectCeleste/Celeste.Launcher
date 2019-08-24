using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo;
using System.Threading;
using System.Threading.Tasks;

namespace Celeste_Public_Api.WebSocket_Api.WebSocket
{
    public class AwaitableOperation<TResponse> where TResponse : IGenericResponse
    {
        private SemaphoreSlim _semaphore = new SemaphoreSlim(0, 1);

        private TResponse _response;

        public void ReceivedMessage(TResponse response)
        {
            _response = response;
            _semaphore.Release();
        }

        public async Task<TResponse> WaitForResponseAsync(int timeoutInSeconds)
        {
            await _semaphore.WaitAsync(timeoutInSeconds * 1000);
            return _response;
        }
    }
}
