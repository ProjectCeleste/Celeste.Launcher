#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Enum;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket
{
    public class DataExchange
    {
        public DataExchange(Client webSocketClient, string cmdName)
        {
            CmdName = cmdName;
            WebSocketClient = webSocketClient;
        }

        private string CmdName { get; }

        private Client WebSocketClient { get; }

        private dynamic RequestResult { get; set; }

        private RequestState RequestState { get; set; } = RequestState.Idle;

        public async Task<dynamic> DoDataExchange(dynamic content)
        {
            dynamic retVal;

            if (RequestState == RequestState.InProgress)
                throw new Exception(@"Request already in progress!");

            RequestState = RequestState.InProgress;

            WebSocketClient.Agent.Query<dynamic>(CmdName, (object) content, OnDataExchange);

            var starttime = DateTime.UtcNow;
            while (RequestState == RequestState.InProgress && WebSocketClient.State == ClientState.Connected)
            {
                var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                if (diff < Client.TimeOut)
                    continue;

                RequestState = RequestState.TimedOut;

                throw new Exception($"Server response timeout ({diff}s)!");
            }

            if (RequestState == RequestState.Success)
            {
                retVal = RequestResult;
            }
            else
            {
                if (RequestResult != null)
                {
                    var msg = RequestResult["Message"].ToObject<string>();
                    if (!string.IsNullOrEmpty(msg))
                        throw new Exception(msg);
                }

                if (!string.IsNullOrEmpty(WebSocketClient.ErrorMessage))
                {
                    throw new Exception(WebSocketClient.ErrorMessage);
                }

                throw new Exception("Unknow error!");
            }

            await Task.Delay(200).ConfigureAwait(false);

            return retVal;
        }

        private void OnDataExchange(dynamic result)
        {
            RequestResult = result;
            RequestState = RequestResult["Result"].ToObject<bool>() ? RequestState.Success : RequestState.Failed;
        }
    }
}