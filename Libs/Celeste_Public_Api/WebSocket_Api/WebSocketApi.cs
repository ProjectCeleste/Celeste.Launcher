#region Using directives

using System;
using System.Threading;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.WebSocket_Api.WebSocket;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Command;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Enum;

#endregion

namespace Celeste_Public_Api.WebSocket_Api
{
    public class WebSocketApi
    {
        private readonly ChangePwd _changePwd;

        private readonly Client _client;

        private readonly ForgotPwd _forgotPwd;

        private readonly Login _login;

        private readonly Register _register;

        private readonly ResetPwd _resetPwd;

        private readonly ValidMail _validMail;

        private readonly Version _apiVersion = new Version(2, 0, 1, 5);

        private DateTime _lastActivity = DateTime.UtcNow;

        private bool _loggedIn;

        private LoginRequest _loginRequest;

        public WebSocketApi(string uri)
        {
            _client = new Client(uri);
            _login = new Login(_client);
            _changePwd = new ChangePwd(_client);
            _forgotPwd = new ForgotPwd(_client);
            _resetPwd = new ResetPwd(_client);
            _validMail = new ValidMail(_client);
            _register = new Register(_client);
        }

        public bool Connected => _client?.State == ClientState.Connected;

        public bool LoggedIn
        {
            get => Connected && _loggedIn;
            private set => _loggedIn = value;
        }

        public async Task Connect()
        {
            if (_client.State != ClientState.Connected || _client.State != ClientState.Connecting)
            {
                await _client.DoConnect();
                _lastActivity = DateTime.UtcNow;
                StartDisconnectIdleSessionTimer();
            }
        }

        public void Disconnect()
        {
            try
            {
                StopDisconnectIdleSessionTimer();
                _loggedIn = false;
                _client.Disconnect();
            }
            catch
            {
                //
            }
        }

        public async Task<LoginResponse> DoLogin(string eMail, string password)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new LoginRequest
            {
                Mail = eMail,
                Password = password,
                Version = _apiVersion,
                FingerPrint = FingerPrint.Value()
            };

            var response = await _login.DoLogin(request);

            if (response.Result)
            {
                LoggedIn = true;
                _loginRequest = request;
            }
            else
            {
                LoggedIn = false;
                _loginRequest = null;

                try
                {
                    Disconnect();
                }
                catch (Exception)
                {
                    //
                }
            }

            return response;
        }

        private async Task<LoginResponse> DoReLogin()
        {
            if (_loginRequest == null)
                return new LoginResponse {Result = false, Message = "Invalid stored login information!"};
            
            return await DoLogin(_loginRequest.Mail, _loginRequest.Password);
        }

        public async Task<ChangePwdResponse> DoChangePassword(string oldPwd, string newPwd)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new ChangePwdRequest
            {
                Old = oldPwd,
                New = newPwd
            };

            if (LoggedIn)
                return await _changePwd.DoChangePwd(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new ChangePwdResponse {Result = false, Message = loginResponse.Message};

            return await _changePwd.DoChangePwd(request);
        }

        public async Task<ForgotPwdResponse> DoForgotPwd(string eMail)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ForgotPwdRequest
            {
                Version = _apiVersion,
                EMail = eMail
            };

            var response = await _forgotPwd.DoForgotPwd(request);

            return response;
        }

        public async Task<ResetPwdResponse> DoResetPwd(string eMail, string verifyKey)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ResetPwdRequest
            {
                Version = _apiVersion,
                EMail = eMail,
                VerifyKey = verifyKey
            };

            var response = await _resetPwd.DoResetPwd(request);

            return response;
        }

        public async Task<ValidMailResponse> DoValidMail(string eMail)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ValidMailRequest
            {
                Version = _apiVersion,
                EMail = eMail
            };

            var response = await _validMail.DoValidMail(request);

            return response;
        }

        public async Task<RegisterResponse> DoRegister(string eMail, string verifyKey, string username, string password)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new RegisterRequest
            {
                Version = _apiVersion,
                Mail = eMail,
                VerifyKey = verifyKey,
                UserName = username,
                Password = password,
                FingerPrint = FingerPrint.Value()
            };

            var response = await _register.DoRegister(request);

            return response;
        }

        #region Disconnect Idle Session

        private const int TimerInterval = 5 * 60 * 1000; // Every 5 min

        private const int TimeOutMs = 3 * 60 * 1000; // 3 min

        private Timer _disconnectIdleSession;

        private readonly object _disconnectIdleSessionSyncLock = new object();

        private void StartDisconnectIdleSessionTimer()
        {
            if (!Monitor.TryEnter(_disconnectIdleSessionSyncLock))
                return;

            try
            {
                if (_disconnectIdleSession == null)
                    _disconnectIdleSession = new Timer(DisconnectIdleSession, new object(), TimerInterval, TimerInterval);

            }
            catch
            {
                //
            }
            finally
            {
                Monitor.Exit(_disconnectIdleSessionSyncLock);
            }
        }

        private void StopDisconnectIdleSessionTimer()
        {
            if (_disconnectIdleSession == null)
                return;

            _disconnectIdleSession.Change(Timeout.Infinite, Timeout.Infinite);
            _disconnectIdleSession.Dispose();
            _disconnectIdleSession = null;
        }

        private void DisconnectIdleSession(object state)
        {
            if (!Monitor.TryEnter(state))
                return;

            try
            {
                var timeOut = DateTime.UtcNow.AddMilliseconds(-TimeOutMs);

                if (_lastActivity <= timeOut)
                    Disconnect();
            }
            catch
            {
                //
            }
            finally
            {
                Monitor.Exit(state);
            }
        }

        #endregion
    }
}