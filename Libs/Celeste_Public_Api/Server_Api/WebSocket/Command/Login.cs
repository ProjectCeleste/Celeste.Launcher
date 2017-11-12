#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Server_Api.WebSocket.Enum;
using Celeste_Public_Api.Server_Api.WebSocket.Packet.Request;
using Celeste_Public_Api.Server_Api.WebSocket.Packet.Response;

#endregion

namespace Celeste_Public_Api.Server_Api.WebSocket.Command
{
    public class Login
    {
        public const string CmdName = "LOGIN";

        private LoginResponse _requestResult;

        private RequestState _requestState = RequestState.Idle;

        public async Task<LoginResponse> DoLogin(Client webSocketClient, string email, string password,
            Version version)
        {
            LoginResponse retVal;
            try
            {
                if (_requestState == RequestState.InProgress)
                    throw new Exception(@"Login already in progress!");

                _requestState = RequestState.InProgress;

                dynamic loginInfo = new LoginRequest
                {
                    Mail = email,
                    Password = password,
                    Version = version,
                    FingerPrint = null
                };

                webSocketClient.Agent.Query<dynamic>(CmdName, (object) loginInfo, OnLoggedIn);

                var starttime = DateTime.UtcNow;
                while (_requestState == RequestState.InProgress && webSocketClient.State == ClientState.Connected)
                {
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff < Client.TimeOut)
                        continue;

                    _requestState = RequestState.TimedOut;
                    throw new Exception($"DoLogin() Server response timeout ({diff}s)!");
                }

                if (_requestState == RequestState.Success)
                {
                    retVal = _requestResult;
                }
                else
                {
                    _requestState = RequestState.Failed;

                    if (string.IsNullOrEmpty(_requestResult?.Message))
                        throw new Exception(webSocketClient.ErrorMessage);

                    throw new Exception(_requestResult.Message);
                }
            }
            catch (Exception ex)
            {
                retVal = new LoginResponse
                {
                    Result = false,
                    Message = ex.Message
                };
            }

            await Task.Delay(200).ConfigureAwait(false);
            return retVal;
        }

        private void OnLoggedIn(dynamic result)
        {
            _requestResult = result.ToObject<LoginResponse>();
            _requestState = _requestResult.Result ? RequestState.Success : RequestState.Failed;
        }
    }
}