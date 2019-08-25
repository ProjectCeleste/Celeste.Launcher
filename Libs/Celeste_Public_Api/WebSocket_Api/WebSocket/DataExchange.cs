#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Enum;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket
{
    public class DataExchange
    {
        private readonly Client _webSocketClient;

        public DataExchange(Client webSocketClient)
        {
            _webSocketClient = webSocketClient;
        }

        private RequestState _requestState = RequestState.Idle;

        public async Task<TResponse> DoDataExchange<TResponse, TRequest>(TRequest content, string cmdName)
            where TResponse : IGenericResponse
        {
            if (_requestState == RequestState.InProgress)
                throw new Exception(@"Request already in progress!");

            _requestState = RequestState.InProgress;

            var responseAction = new AwaitableOperation<TResponse>();

            _webSocketClient.Query<TResponse>(cmdName, content, responseAction.ReceivedMessage);

            try
            {
                var response = await responseAction.WaitForResponseAsync(Client.ConnectionTimeoutInMilliseconds);

                if (!response.Result)
                {
                    if (string.IsNullOrWhiteSpace(response.Message))
                        throw new Exception(response.Message);

                    if (!string.IsNullOrEmpty(_webSocketClient.ErrorMessage))
                        throw new Exception(_webSocketClient.ErrorMessage);

                    throw new Exception("Unknow error!");
                }

                return response;
            }
            finally
            {
                _requestState = RequestState.Idle;
            }
        }
    }
}