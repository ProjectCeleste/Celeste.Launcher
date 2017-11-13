#region Using directives

using System;
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
        private readonly Version _version = new Version(2, 0, 0, 0);

        private bool _loggedIn;

        public WebSocketApi(string uri)
        {
            Client = new Client(uri);
            Login = new Login(Client);
            ChangePwd = new ChangePwd(Client);
            ForgotPwd = new ForgotPwd(Client);
            ResetPwd = new ResetPwd(Client);
            ValidMail = new ValidMail(Client);
            Register = new Register(Client);
        }

        private Client Client { get; }

        private Login Login { get; }

        private LoginRequest LoginRequest { get; set; }

        private ChangePwd ChangePwd { get; }

        private ForgotPwd ForgotPwd { get; }

        private ResetPwd ResetPwd { get; }

        private ValidMail ValidMail { get; }

        private Register Register { get; }

        public bool LoggedIn
        {
            get => Client?.State == ClientState.Connected && _loggedIn;
            private set => _loggedIn = value;
        }

        public void Disconnect()
        {
            try
            {
                Client?.Agent?.Close();
            }
            catch
            {
                //
            }
        }

        public async Task<LoginResponse> DoLogin(string eMail, string password)
        {
            if (Client.State != ClientState.Connected)
                await Client.DoConnect();

            var request = new LoginRequest
            {
                Mail = eMail,
                Password = password,
                Version = _version,
                FingerPrint = FingerPrint.Value()
            };

            var response = await Login.DoLogin(request);

            if (response.Result)
            {
                LoggedIn = true;
                LoginRequest = request;
            }
            else
            {
                LoggedIn = false;
                LoginRequest = null;
            }

            return response;
        }

        private async Task<LoginResponse> DoReLogin()
        {
            if (LoginRequest == null)
                return new LoginResponse {Result = false, Message = "Invalid login information!"};

            if (Client.State != ClientState.Connected)
                await Client.DoConnect();

            var response = await Login.DoLogin(LoginRequest);

            if (response.Result)
            {
                LoggedIn = true;
            }
            else
            {
                LoggedIn = false;
                LoginRequest = null;
            }

            return response;
        }

        public async Task<ChangePwdResponse> DoChangePassword(string oldPwd, string newPwd)
        {
            var request = new ChangePwdRequest
            {
                Old = oldPwd,
                New = newPwd
            };

            if (LoggedIn)
                return await ChangePwd.DoChangePwd(request);

            var loginResponse = await DoReLogin();
            if (!loginResponse.Result)
                return new ChangePwdResponse {Result = false, Message = loginResponse.Message};

            return await ChangePwd.DoChangePwd(request);
        }

        public async Task<ForgotPwdResponse> DoForgotPwd(string eMail)
        {
            if (Client.State != ClientState.Connected)
                await Client.DoConnect();

            var request = new ForgotPwdRequest
            {
                Version = _version,
                EMail = eMail
            };

            var response = await ForgotPwd.DoForgotPwd(request);

            return response;
        }

        public async Task<ResetPwdResponse> DoResetPwd(string eMail, string verifyKey)
        {
            if (Client.State != ClientState.Connected)
                await Client.DoConnect();

            var request = new ResetPwdRequest
            {
                Version = _version,
                EMail = eMail,
                VerifyKey = verifyKey
            };

            var response = await ResetPwd.DoResetPwd(request);

            return response;
        }

        public async Task<ValidMailResponse> DoValidMail(string eMail)
        {
            if (Client.State != ClientState.Connected)
                await Client.DoConnect();

            var request = new ValidMailRequest
            {
                Version = _version,
                EMail = eMail
            };

            var response = await ValidMail.DoValidMail(request);

            return response;
        }

        public async Task<RegisterResponse> DoRegister(string eMail, string verifyKey, string username, string password)
        {
            if (Client.State != ClientState.Connected)
                await Client.DoConnect();

            var request = new RegisterRequest
            {
                Version = _version,
                Mail = eMail,
                VerifyKey = verifyKey,
                UserName = username,
                Password = password,
                FingerPrint = FingerPrint.Value()
            };

            var response = await Register.DoRegister(request);

            return response;
        }
    }
}