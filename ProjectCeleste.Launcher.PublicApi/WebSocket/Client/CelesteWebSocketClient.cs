#region Using directives

using System;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using ProjectCeleste.Launcher.PublicApi.Helpers;
using ProjectCeleste.Launcher.PublicApi.WebSocket.Client.Enum;
using ProjectCeleste.Launcher.PublicApi.WebSocket.Client.WebSocket4Net;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Enum;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest.Password;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Guest.Register;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Friends;
using ProjectCeleste.Launcher.PublicApi.WebSocket.CommandInfo.Model.Member.Password;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.WebSocket.Client
{
    public class CelesteWebSocketClient
    {
        private readonly Version _apiVersion;

        private readonly WebSocket4Net.Client _client;

        private DateTime _lastActivity;

        private bool _loggedIn;

        private string _currentEmail;

        private SecureString _currentPassword;

        public CelesteWebSocketClient(string uri)
        {
            _apiVersion = Assembly.GetAssembly(typeof(CelesteWebSocketClient)).GetName().Version;

            _client = new WebSocket4Net.Client(uri);

            _login = new CommandBase<LoginInfo, LoginResponse>(_client, LoginInfo.CommandName);
            _changePwd = new CommandBase<ChangePwdRequest, ChangePwdResponse>(_client, ChangePwdRequest.CommandName);
            _forgotPwd =
                new CommandBase<ForgotPwdRequest, ForgotPwdResponse>(_client, ForgotPwdRequest.CommandName, true);
            _resetPwd = new CommandBase<ResetPwdRequest, ResetPwdResponse>(_client, ResetPwdRequest.CommandName, true);
            _validMail =
                new CommandBase<ValidMailRequest, ValidMailResponse>(_client, ValidMailRequest.CommandName, true);
            _register = new CommandBase<RegisterUserRequest, RegisterUserResponse>(_client,
                RegisterUserRequest.CommandName, true);
            _getFriends =
                new CommandBase<GetFriendsRequest, GetFriendsResponse>(_client, GetFriendsRequest.CommandName, true,
                    30);
            _getPFriends =
                new CommandBase<GetPendingFriendsRequest, GetPendingFriendsResponse>(_client,
                    GetPendingFriendsRequest.CommandName, true, 30);
            _removeFriend =
                new CommandBase<RemoveFriendRequest, RemoveFriendResponse>(_client, RemoveFriendRequest.CommandName);
            _addFriend = new CommandBase<AddFriendRequest, AddFriendResponse>(_client, AddFriendRequest.CommandName);
            _confirmFriend =
                new CommandBase<ConfirmFriendRequest, ConfirmFriendResponse>(_client, ConfirmFriendRequest.CommandName);

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
            StopDisconnectIdleSessionTimer();
            _loggedIn = false;
            _client.Disconnect();
        }

        #region Command

        private readonly CommandBase<AddFriendRequest, AddFriendResponse> _addFriend;

        private readonly CommandBase<ChangePwdRequest, ChangePwdResponse> _changePwd;

        private readonly CommandBase<ConfirmFriendRequest, ConfirmFriendResponse> _confirmFriend;

        private readonly CommandBase<ForgotPwdRequest, ForgotPwdResponse> _forgotPwd;

        private readonly CommandBase<GetFriendsRequest, GetFriendsResponse> _getFriends;

        private readonly CommandBase<GetPendingFriendsRequest, GetPendingFriendsResponse> _getPFriends;

        private readonly CommandBase<LoginInfo, LoginResponse> _login;

        private readonly CommandBase<RegisterUserRequest, RegisterUserResponse> _register;

        private readonly CommandBase<RemoveFriendRequest, RemoveFriendResponse> _removeFriend;

        private readonly CommandBase<ResetPwdRequest, ResetPwdResponse> _resetPwd;

        private readonly CommandBase<ValidMailRequest, ValidMailResponse> _validMail;

        #region Login

        public async Task<LoginResponse> DoLogin(string email, SecureString password)
        {
            if (!IsValidString.IsValidEmailAddress(email))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            var passwordUnsecure = password.GetValue();
            if (!IsValidString.IsValidPasswordLength(passwordUnsecure))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordLength);
                throw ex;
            }

            if (!IsValidString.IsValidPassword(passwordUnsecure))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPassword);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var fingerPrint = await FingerPrintProvider.GetFingerprintAsync();

            var request = new LoginInfo(email, passwordUnsecure, _apiVersion, fingerPrint);

            var response = await _login.Execute(request);

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

                Disconnect();
            }

            return response;
        }

        private async Task<LoginResponse> DoReLogin()
        {
            if (string.IsNullOrWhiteSpace(_currentEmail) || _currentPassword == null)
                return LoginResponse.FailResponse("Invalid stored login information");

            return await DoLogin(_currentEmail, _currentPassword);
        }

        #endregion Login

        #region Password

        public async Task<ChangePwdResponse> DoChangePassword(string oldPwd, string newPwd)
        {
            if (string.Equals(oldPwd, newPwd, StringComparison.CurrentCulture))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordMatch}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordMatch);
                throw ex;
            }

            if (!IsValidString.IsValidPasswordLength(oldPwd))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordLength);
                throw ex;
            }

            if (!IsValidString.IsValidPassword(oldPwd))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPassword);
                throw ex;
            }

            if (!IsValidString.IsValidPasswordLength(newPwd))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidNewPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidNewPasswordLength);
                throw ex;
            }

            if (!IsValidString.IsValidPassword(newPwd))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidNewPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidNewPassword);
                throw ex;
            }

            _lastActivity = DateTime.UtcNow;

            var request = new ChangePwdRequest(oldPwd, newPwd);

            if (LoggedIn)
                return await _changePwd.Execute(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return ChangePwdResponse.FailResponse(loginResponse.ErrorCode, loginResponse.Message);

            return await _changePwd.Execute(request);
        }

        public async Task<ForgotPwdResponse> DoForgotPwd(string eMail)
        {
            if (!IsValidString.IsValidEmailAddress(eMail))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ForgotPwdRequest(_apiVersion, eMail);

            return await _forgotPwd.Execute(request);
        }

        public async Task<ResetPwdResponse> DoResetPwd(string eMail, string verifyKey)
        {
            if (!IsValidString.IsValidEmailAddress(eMail))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (!IsValidString.IsValidVerifyKey(verifyKey))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidVerifyKey}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidVerifyKey);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ResetPwdRequest(_apiVersion, eMail, verifyKey);

            return await _resetPwd.Execute(request);
        }

        #endregion Password

        #region Register

        public async Task<ValidMailResponse> DoValidMail(string eMail)
        {
            if (!IsValidString.IsValidEmailAddress(eMail))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var request = new ValidMailRequest(_apiVersion, eMail);

            return await _validMail.Execute(request);
        }

        public async Task<RegisterUserResponse> DoRegister(string eMail, string verifyKey, string username,
            string password)
        {
            if (!IsValidString.IsValidEmailAddress(eMail))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidEmail}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidEmail);
                throw ex;
            }

            if (!IsValidString.IsValidVerifyKey(verifyKey))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidVerifyKey}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidVerifyKey);
                throw ex;
            }

            if (!IsValidString.IsValidUserNameLength(username))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsernameLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsernameLength);
                throw ex;
            }

            if (!IsValidString.IsValidUserName(username))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsername}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsername);
                throw ex;
            }

            if (!IsValidString.IsValidPasswordLength(password))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPasswordLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPasswordLength);
                throw ex;
            }

            if (!IsValidString.IsValidPassword(password))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidPassword}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidPassword);
                throw ex;
            }

            if (_client.State != ClientState.Connected)
                await Connect();

            _lastActivity = DateTime.UtcNow;

            var fingerPrint = await FingerPrintProvider.GetFingerprintAsync();

            var request = new RegisterUserRequest(_apiVersion, eMail, verifyKey, username, password, fingerPrint);

            return await _register.Execute(request);
        }

        #endregion Register

        #region Friends

        public async Task<GetFriendsResponse> DoGetFriends()
        {
            _lastActivity = DateTime.UtcNow;

            var request = new GetFriendsRequest();

            if (LoggedIn)
                return await _getFriends.Execute(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return GetFriendsResponse.FailResponse(loginResponse.ErrorCode, loginResponse.Message);

            return await _getFriends.Execute(request);
        }

        public async Task<GetPendingFriendsResponse> DoGetPendingFriends()
        {
            _lastActivity = DateTime.UtcNow;

            var request = new GetPendingFriendsRequest();

            if (LoggedIn)
                return await _getPFriends.Execute(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return GetPendingFriendsResponse.FailResponse(loginResponse.ErrorCode, loginResponse.Message);

            return await _getPFriends.Execute(request);
        }

        public async Task<RemoveFriendResponse> DoRemoveFriend(long xuid)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new RemoveFriendRequest(xuid);

            if (LoggedIn)
                return await _removeFriend.Execute(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return RemoveFriendResponse.FailResponse(loginResponse.ErrorCode, loginResponse.Message);

            return await _removeFriend.Execute(request);
        }

        public async Task<AddFriendResponse> DoAddFriend(string friendName)
        {
            if (!IsValidString.IsValidUserNameLength(friendName))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsernameLength}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsernameLength);
                throw ex;
            }

            if (!IsValidString.IsValidUserName(friendName, true))
            {
                var ex = new Exception($"ErrorCode: {CommandErrorCode.InvalidUsername}");
                ex.Data.Add("ErrorCode", CommandErrorCode.InvalidUsername);
                throw ex;
            }

            _lastActivity = DateTime.UtcNow;

            var request = new AddFriendRequest(friendName);

            if (LoggedIn)
                return await _addFriend.Execute(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return AddFriendResponse.FailResponse(loginResponse.ErrorCode, loginResponse.Message);

            return await _addFriend.Execute(request);
        }

        public async Task<ConfirmFriendResponse> DoConfirmFriend(long xuid)
        {
            _lastActivity = DateTime.UtcNow;

            var request = new ConfirmFriendRequest(xuid);

            if (LoggedIn)
                return await _confirmFriend.Execute(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return ConfirmFriendResponse.FailResponse(loginResponse.ErrorCode, loginResponse.Message);

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
            if (_disconnectIdleSession == null)
                _disconnectIdleSession = new Timer(DisconnectIdleSession, null, TimerInterval, TimerInterval);
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
            var timeOut = DateTime.UtcNow.AddMilliseconds(-TimeOutMs);

            if (_lastActivity <= timeOut)
                Disconnect();
        }

        #endregion Disconnect Idle Session
    }
}