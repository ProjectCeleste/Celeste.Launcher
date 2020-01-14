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
        public readonly bool PreventSpam;

        public readonly int PreventSpamTimeInSec;

        public readonly string CmdName;

        private readonly Client _webSocketClient;

        private DateTime _lastTime;

        public CommandBase(Client webSocketClient, string cmdName, bool preventSpam = false,
            int preventSpamTimeInSec = 60)
        {
            _webSocketClient = webSocketClient;
            CmdName = cmdName;
            PreventSpam = preventSpam;
            PreventSpamTimeInSec = preventSpamTimeInSec;
            _lastTime = DateTime.MinValue;
        }

        public async Task<TResponse> Execute(TRequest request)
        {
            var lastTimeSec = (DateTime.UtcNow - _lastTime).TotalSeconds;
            if (PreventSpam && lastTimeSec <= PreventSpamTimeInSec)
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidRequestSpam}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidRequestSpam);
                ex.Data.Add("TimeLeftInSec", PreventSpamTimeInSec - lastTimeSec);
                throw ex;
            }

            var result = await DoDataExchange(_webSocketClient, request, CmdName);

            if (result.Result && PreventSpam)
                _lastTime = DateTime.UtcNow;

            return result;
        }

        private static async Task<TResponse> DoDataExchange(Client webSocketClient, TRequest content, string cmdName)
        {
            using var responseAction = new AwaitableOperation<TResponse>();

            webSocketClient.Query<TResponse>(cmdName, content, responseAction.ReceivedMessage);

            var response = await responseAction.WaitForResponseAsync(Client.ConnectionTimeoutInMilliseconds);

            if (response.Result)
                return response;

            if (!string.IsNullOrWhiteSpace(webSocketClient.ErrorMessage))
                throw new Exception(webSocketClient.ErrorMessage);

            if (!string.IsNullOrWhiteSpace(response.Message))
            {
                if (!System.Enum.TryParse(response.Message, out CommandErrorCode code))
                    throw new Exception(response.Message);

                var exM = new Exception($"ErrorCode: {code}");
                exM.Data.Add("ErrorCode", code);
                throw exM;
            }

            var ex = new Exception($"ErrorCode: {CommandErrorCode.Unknow}");
            ex.Data.Add("ErrorCode", CommandErrorCode.Unknow);
            throw ex;
        }
    }
}