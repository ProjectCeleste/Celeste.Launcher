#region Using directives

using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Enum;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.Interface;
using System;
using System.Threading.Tasks;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api
{
    internal class CommandBase<TRequest, TResponse> where TResponse : IGenericResponse
    {
        public readonly bool _preventSpam;

        public readonly int _preventSpamTimeInSec;

        public readonly string _cmdName;

        private readonly Client _webSocketClient;

        private DateTime _lastTime;

        public CommandBase(Client webSocketClient, string cmdName, bool preventSpam = false, int preventSpamTimeInSec = 60)
        {
            _webSocketClient = webSocketClient;
            _cmdName = cmdName;
            _preventSpam = preventSpam;
            _preventSpamTimeInSec = preventSpamTimeInSec;
            _lastTime = DateTime.MinValue;
        }

        public async Task<TResponse> Execute(TRequest request)
        {
            double _lastTimeSec = (DateTime.UtcNow - _lastTime).TotalSeconds;
            if (_preventSpam && _lastTimeSec <= _preventSpamTimeInSec)
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidRequestSpam}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidRequestSpam);
                ex.Data.Add("TimeLeftInSec", _preventSpamTimeInSec - _lastTimeSec);
                throw ex;
            }

            TResponse result = await DoDataExchange(_webSocketClient, request, _cmdName);

            if (result.Result && _preventSpam)
                _lastTime = DateTime.UtcNow;

            return result;
        }

        private static async Task<TResponse> DoDataExchange(Client webSocketClient, TRequest content, string cmdName)
        {
            using AwaitableOperation<TResponse> responseAction = new AwaitableOperation<TResponse>();
            
            webSocketClient.Query<TResponse>(cmdName, content, responseAction.ReceivedMessage);

            TResponse response = await responseAction.WaitForResponseAsync(Client.ConnectionTimeoutInMilliseconds);

            if (!response.Result)
            {
                if (!string.IsNullOrWhiteSpace(webSocketClient.ErrorMessage))
                    throw new Exception(webSocketClient.ErrorMessage);

                if (!string.IsNullOrWhiteSpace(response.Message))
                {
                    if (System.Enum.TryParse(response.Message, out CommandErrorCode code))
                    {
                        Exception exM = new Exception($"ErrorCode: {code}");
                        exM.Data.Add("ErrorCode", code);
                        throw exM;
                    }
                    else
                    {
                        throw new Exception(response.Message);
                    }
                }

                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.Unknow}");
                ex.Data.Add("ErrorCode", CommandErrorCode.Unknow);
                throw ex;
            }

            return response;
        }
    }
}