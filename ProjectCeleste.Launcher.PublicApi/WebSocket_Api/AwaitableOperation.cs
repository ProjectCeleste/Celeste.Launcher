using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api
{
    internal class AwaitableOperation<TResponse> : IDisposable where TResponse : IGenericResponse
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
                throw new Exception("Operation timed out");

            return _response;
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _semaphore.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}