#region Using directives

using ProjectCeleste.Launcher.PublicApi.Helpers;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Command;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.CommandInfo.Member;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.CommandInfo.NotLogged;
using ProjectCeleste.Launcher.PublicApi.WebSocket_Api.WebSocket.Enum;
using System;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket_Api
{
    public class WebSocketApi
    {
        private readonly Version _apiVersion;

        private readonly Client _client;

        private DateTime _lastActivity;

        private bool _loggedIn;

        private string _currentEmail;

        private SecureString _currentPassword;

        public WebSocketApi(string uri)
        {
            _apiVersion = Assembly.GetAssembly(typeof(WebSocketApi)).GetName().Version;

            _client = new Client(uri);

            _login = new CommandBase<LoginInfo, LoginResult>(_client, "LOGIN");
            _changePwd = new CommandBase<ChangePwdInfo, ChangePwdResult>(_client, "CHANGEPWD");
            _forgotPwd = new CommandBase<ForgotPwdInfo, ForgotPwdResult>(_client, "FORGOTPWD", true);
            _resetPwd = new CommandBase<ResetPwdInfo, ResetPwdResult>(_client, "RESETPWD", true);
            _validMail = new CommandBase<ValidMailInfo, ValidMailResult>(_client, "VALIDMAIL", true);
            _register = new CommandBase<RegisterUserInfo, RegisterUserResult>(_client, "REGISTER", true);
            _getFriends = new CommandBase<GetFriendsInfo, GetFriendsResult>(_client, "GETFRIENDS", true, 30);
            _getPFriends = new CommandBase<GetPendingFriendsInfo, GetPendingFriendsResult>(_client, "GETPFRIENDS", true, 30);
            _removeFriend = new CommandBase<RemoveFriendInfo, RemoveFriendResult>(_client, "REMFRIEND");
            _addFriend = new CommandBase<AddFriendInfo, AddFriendResult>(_client, "ADDFRIEND");
            _confirmFriend = new CommandBase<ConfirmFriendInfo, ConfirmFriendResult>(_client, "CONFFRIEND");

            _lastActivity = DateTime.UtcNow;
        }

        public bool Connected => _client.State == ClientState.Connected;

        public bool LoggedIn
        {
            get => Connected && _loggedIn;
            private set => _loggedIn = value;
        }

        private async Task Connect()
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

        #region Command

        private readonly CommandBase<AddFriendInfo, AddFriendResult> _addFriend;

        private readonly CommandBase<ChangePwdInfo, ChangePwdResult> _changePwd;

        private readonly CommandBase<ConfirmFriendInfo, ConfirmFriendResult> _confirmFriend;

        private readonly CommandBase<ForgotPwdInfo, ForgotPwdResult> _forgotPwd;

        private readonly CommandBase<GetFriendsInfo, GetFriendsResult> _getFriends;

        private readonly CommandBase<GetPendingFriendsInfo, GetPendingFriendsResult> _getPFriends;

        private readonly CommandBase<LoginInfo, LoginResult> _login;

        private readonly CommandBase<RegisterUserInfo, RegisterUserResult> _register;

        private readonly CommandBase<RemoveFriendInfo, RemoveFriendResult> _removeFriend;

        private readonly CommandBase<ResetPwdInfo, ResetPwdResult> _resetPwd;

        private readonly CommandBase<ValidMailInfo, ValidMailResult> _validMail;

        #region Login

        public async Task<LoginResult> DoLogin(string email, SecureString password)
        {
            if (!IsValidString.IsValidEmailAdress(email))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            string passwordUnsecure = password.GetValue();
            if (!IsValidString.IsValidPasswordLength(passwordUnsecure))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordLength);
                throw ex;
            }
            if (!IsValidString.IsValidPassword(passwordUnsecure))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPassword);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            string fingerPrint = await FingerPrintProvider.GetFingerprintAsync();

            LoginInfo request = new LoginInfo(email, passwordUnsecure, _apiVersion, fingerPrint);

            LoginResult response = await _login.Execute(request);

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
            if (string.IsNullOrWhiteSpace(_currentEmail) || _currentPassword == null)
                return new LoginResult(false, "Invalid stored login information");

            return await DoLogin(_currentEmail, _currentPassword);
        }

        #endregion Login

        #region Password

        public async Task<ChangePwdResult> DoChangePassword(string oldPwd, string newPwd)
        {
            if (string.Equals(oldPwd, newPwd, StringComparison.CurrentCulture))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordMatch}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordMatch);
                throw ex;
            }

            if (!IsValidString.IsValidPasswordLength(oldPwd))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordLength);
                throw ex;
            }
            if (!IsValidString.IsValidPassword(oldPwd))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPassword);
                throw ex;
            }

            if (!IsValidString.IsValidPasswordLength(newPwd))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidNewPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidNewPasswordLength);
                throw ex;
            }
            if (!IsValidString.IsValidPassword(newPwd))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidNewPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidNewPassword);
                throw ex;
            }

            _lastActivity = DateTime.UtcNow;

            ChangePwdInfo request = new ChangePwdInfo(oldPwd, newPwd);

            if (LoggedIn)
                return await _changePwd.Execute(request);

            LoginResult loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new ChangePwdResult(false, loginResponse.Message);

            return await _changePwd.Execute(request);
        }

        public async Task<ForgotPwdResult> DoForgotPwd(string eMail)
        {
            if (!IsValidString.IsValidEmailAdress(eMail))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            ForgotPwdInfo request = new ForgotPwdInfo(_apiVersion, eMail);

            return await _forgotPwd.Execute(request);
        }

        public async Task<ResetPwdResult> DoResetPwd(string eMail, string verifyKey)
        {
            if (!IsValidString.IsValidEmailAdress(eMail))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (!IsValidString.IsValidVerifyKey(verifyKey))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidVerifyKey}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidVerifyKey);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            ResetPwdInfo request = new ResetPwdInfo(_apiVersion, eMail, verifyKey);

            return await _resetPwd.Execute(request);
        }

        #endregion Password

        #region Register

        public async Task<ValidMailResult> DoValidMail(string eMail)
        {
            if (!IsValidString.IsValidEmailAdress(eMail))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            ValidMailInfo request = new ValidMailInfo(_apiVersion, eMail);

            return await _validMail.Execute(request);
        }

        public async Task<RegisterUserResult> DoRegister(string eMail, string verifyKey, string username, string password)
        {
            if (!IsValidString.IsValidEmailAdress(eMail))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (!IsValidString.IsValidVerifyKey(verifyKey))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidVerifyKey}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidVerifyKey);
                throw ex;
            }

            if (!IsValidString.IsValidUserNameLength(username))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsernameLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsernameLength);
                throw ex;
            }
            if (!IsValidString.IsValidUserName(username))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsername}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsername);
                throw ex;
            }

            if (!IsValidString.IsValidPasswordLength(password))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordLength);
                throw ex;
            }
            if (!IsValidString.IsValidPassword(password))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPassword);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            string fingerPrint = await FingerPrintProvider.GetFingerprintAsync();

            RegisterUserInfo request = new RegisterUserInfo(_apiVersion, eMail, verifyKey, username, password, fingerPrint);

            return await _register.Execute(request);
        }

        #endregion Register

        #region Friends

        public async Task<GetFriendsResult> DoGetFriends()
        {
            _lastActivity = DateTime.UtcNow;

            GetFriendsInfo request = new GetFriendsInfo();

            if (LoggedIn)
                return await _getFriends.Execute(request);

            LoginResult loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new GetFriendsResult(false, loginResponse.Message);

            return await _getFriends.Execute(request);
        }

        public async Task<GetPendingFriendsResult> DoGetPendingFriends()
        {
            _lastActivity = DateTime.UtcNow;

            GetPendingFriendsInfo request = new GetPendingFriendsInfo();

            if (LoggedIn)
                return await _getPFriends.Execute(request);

            LoginResult loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new GetPendingFriendsResult(false, loginResponse.Message);

            return await _getPFriends.Execute(request);
        }

        public async Task<RemoveFriendResult> DoRemoveFriend(long xuid)
        {
            _lastActivity = DateTime.UtcNow;

            RemoveFriendInfo request = new RemoveFriendInfo(xuid);

            if (LoggedIn)
                return await _removeFriend.Execute(request);

            LoginResult loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new RemoveFriendResult(false, loginResponse.Message);

            return await _removeFriend.Execute(request);
        }

        public async Task<AddFriendResult> DoAddFriend(string friendName)
        {
            if (!IsValidString.IsValidUserNameLength(friendName))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsernameLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsernameLength);
                throw ex;
            }
            if (!IsValidString.IsValidUserName(friendName, true))
            {
                Exception ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsername}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsername);
                throw ex;
            }

            _lastActivity = DateTime.UtcNow;

            AddFriendInfo request = new AddFriendInfo(friendName);

            if (LoggedIn)
                return await _addFriend.Execute(request);

            LoginResult loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new AddFriendResult(false, loginResponse.Message);

            return await _addFriend.Execute(request);
        }

        public async Task<ConfirmFriendResult> DoConfirmFriend(long xuid)
        {
            _lastActivity = DateTime.UtcNow;

            ConfirmFriendInfo request = new ConfirmFriendInfo(xuid);

            if (LoggedIn)
                return await _confirmFriend.Execute(request);

            LoginResult loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new ConfirmFriendResult(false, loginResponse.Message);

            return await _confirmFriend.Execute(request);
        }

        #endregion Friends

        #endregion Command

        #region Disconnect Idle Session

        private const int TimerInterval = 60 * 1000; // Every 60sec

        private const int TimeOutMs = 5 * 60 * 1000; // 5 min

        private Timer _disconnectIdleSession;

        private void StartDisconnectIdleSessionTimer()
        {
            try
            {
                if (_disconnectIdleSession == null)
                {
                    _disconnectIdleSession = new Timer(DisconnectIdleSession, new object(), TimerInterval,
                        TimerInterval);
                }
            }
            catch
            {
                //
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
                DateTime timeOut = DateTime.UtcNow.AddMilliseconds(-TimeOutMs);

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

        #endregion Disconnect Idle Session
    }
}