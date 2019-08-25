#region Using directives

using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
using Celeste_Public_Api.WebSocket_Api.WebSocket;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Command;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.NotLogged;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Enum;
using Serilog;

#endregion

namespace Celeste_Public_Api.WebSocket_Api
{
    public class WebSocketApi
    {
        private readonly AddFriend _addFriend;

        private readonly Version _apiVersion = new Version(3, 0, 0, 0);
        
		private readonly ChangePwd _changePwd;

        private readonly Client _client;

        private readonly ConfirmFriend _confirmFriend;

        private readonly ForgotPwd _forgotPwd;

        private readonly GetFriends _getFriends;

        private readonly GetPendingFriends _getPFriends;

        private readonly Login _login;

        private readonly Register _register;

        private readonly RemoveFriend _removeFriend;

        private readonly ResetPwd _resetPwd;

        private readonly ValidMail _validMail;

        private DateTime _lastActivity = DateTime.UtcNow;

        private bool _loggedIn;

        private string _currentEmail;
        private SecureString _currentPassword;

        private readonly ILogger _logger;

        public WebSocketApi(string uri)
        {
            _client = new Client(uri);
            var dataExchange = new DataExchange(_client);

            _login = new Login(dataExchange);
            _changePwd = new ChangePwd(dataExchange);
            _forgotPwd = new ForgotPwd(dataExchange);
            _resetPwd = new ResetPwd(dataExchange);
            _validMail = new ValidMail(dataExchange);
            _register = new Register(dataExchange);
            _getFriends = new GetFriends(dataExchange);
            _getPFriends = new GetPendingFriends(dataExchange);
            _removeFriend = new RemoveFriend(dataExchange);
            _addFriend = new AddFriend(dataExchange);
            _confirmFriend = new ConfirmFriend(dataExchange);
            _logger = LoggerFactory.GetLogger();
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

        public async Task<LoginResult> DoLogin(string email, SecureString password)
        {
            if (_client.State != ClientState.Connected)
                await Connect();
            
            _lastActivity = DateTime.UtcNow;

            var fingerPrint = await FingerPrintProvider.GetFingerprintAsync();

            var request = new LoginInfo(email, password.GetValue(), _apiVersion, fingerPrint);

            var response = await _login.DoLogin(request);

            if (response.Result)
            {
                LoggedIn = true;
                _currentEmail = email;
                _currentPassword = password;
            }
            else
            {
                LoggedIn = false;
                _currentEmail = null;
                _currentPassword = null;

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

        private async Task<LoginResult> DoReLogin()
        {
            if (_currentEmail == null || _currentPassword == null)
                return new LoginResult(false, "Invalid stored login information");

            return await DoLogin(_currentEmail, _currentPassword);
        }

        public async Task<ChangePwdResult> DoChangePassword(string oldPwd, string newPwd)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new ChangePwdInfo(oldPwd, newPwd);

            if (LoggedIn)
                return await _changePwd.DoChangePwd(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new ChangePwdResult(false, loginResponse.Message);

            return await _changePwd.DoChangePwd(request);
        }

        public async Task<ForgotPwdResult> DoForgotPwd(string eMail)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ForgotPwdInfo(_apiVersion, eMail);

            var response = await _forgotPwd.DoForgotPwd(request);

            return response;
        }

        public async Task<ResetPwdResult> DoResetPwd(string eMail, string verifyKey)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ResetPwdInfo(_apiVersion, eMail, verifyKey);

            var response = await _resetPwd.DoResetPwd(request);

            return response;
        }

        public async Task<ValidMailResult> DoValidMail(string eMail)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ValidMailInfo(_apiVersion, eMail);

            var response = await _validMail.DoValidMail(request);

            return response;
        }

        public async Task<RegisterUserResult> DoRegister(string eMail, string verifyKey, string username,
            string password)
        {
            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var fingerPrint = await FingerPrintProvider.GetFingerprintAsync();

            var request = new RegisterUserInfo(_apiVersion, eMail, verifyKey, username, password, fingerPrint);

            var response = await _register.DoRegister(request);

            return response;
        }

        public async Task<GetFriendsResult> DoGetFriends()
        {
            _lastActivity = DateTime.UtcNow;

            var request = new GetFriendsInfo();

            if (LoggedIn)
                return await _getFriends.DoGetFriends(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new GetFriendsResult(false, loginResponse.Message);

            return await _getFriends.DoGetFriends(request);
        }

        public async Task<GetPendingFriendsResult> DoGetPendingFriends()
        {
            _lastActivity = DateTime.UtcNow;

            var request = new GetPendingFriendsInfo();

            if (LoggedIn)
                return await _getPFriends.DoGetPendingFriends(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new GetPendingFriendsResult(false, loginResponse.Message);

            return await _getPFriends.DoGetPendingFriends(request);
        }

        public async Task<RemoveFriendResult> DoRemoveFriend(long xuid)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new RemoveFriendInfo(xuid);

            if (LoggedIn)
                return await _removeFriend.DoRemoveFriend(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new RemoveFriendResult(false, loginResponse.Message);

            return await _removeFriend.DoRemoveFriend(request);
        }

        public async Task<AddFriendResult> DoAddFriend(string friendName)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new AddFriendInfo(friendName);

            if (LoggedIn)
                return await _addFriend.DoAddFriend(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new AddFriendResult(false, loginResponse.Message);

            return await _addFriend.DoAddFriend(request);
        }

        public async Task<ConfirmFriendResult> DoConfirmFriend(long xuid)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new ConfirmFriendInfo(xuid);

            if (LoggedIn)
                return await _confirmFriend.DoConfirmFriend(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new ConfirmFriendResult(false, loginResponse.Message);

            return await _confirmFriend.DoConfirmFriend(request);
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
                    _disconnectIdleSession = new Timer(DisconnectIdleSession, new object(), TimerInterval,
                        TimerInterval);
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