#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.Server_Api.Models.User;
using Celeste_Public_Api.Server_Api.WebSocket;
using Celeste_Public_Api.Server_Api.WebSocket.Command;
using Celeste_Public_Api.Server_Api.WebSocket.Enum;

#endregion

namespace Celeste_Public_Api.Server_Api
{
    public class ServerApi
    {
        private readonly Client _client;

        private readonly Login _login = new Login();

        private string _eMail;

        private bool _loggedIn;

        private string _password;

        public ServerApi()
        {
            _client = new Client("");
        }

        public RemoteUser CurrentUser { get; private set; }

        public bool LoggedIn
        {
            get => _client?.State == ClientState.Connected && _loggedIn;
            private set => _loggedIn = value;
        }

        public async Task DoLogin(string email, string password)
        {
            if (_client.State != ClientState.Connected)
                await _client.DoConnect();

            var response = await _login.DoLogin(_client, "", "", new Version());
            if (response.Result)
            {
                _eMail = email;
                _password = password;
                CurrentUser = response.RemoteUser;
                LoggedIn = true;
            }
            else
            {
                LoggedIn = false;
                throw new Exception(response.Message);
            }
        }

        public async Task DoChangePassword()
        {
            if (!LoggedIn)
                await DoLogin(_eMail, _password);

            //TODO
        }

        public async Task DoForgotPwd()
        {
            if (_client.State != ClientState.Connected)
                await _client.DoConnect();

            //TODO
        }

        public async Task DoResetPwd()
        {
            if (_client.State != ClientState.Connected)
                await _client.DoConnect();

            //TODO
        }

        public async Task DoValidMail()
        {
            if (_client.State != ClientState.Connected)
                await _client.DoConnect();

            //TODO
        }

        public async Task DoRegister()
        {
            if (_client.State != ClientState.Connected)
                await _client.DoConnect();

            //TODO
        }
    }
}