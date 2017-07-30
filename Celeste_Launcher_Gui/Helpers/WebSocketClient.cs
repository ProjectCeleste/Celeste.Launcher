#region Using directives

using System;
using System.ComponentModel;
using System.Dynamic;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using Celeste_User.Remote;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using WebSocket4Net;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public class AgentWebSocket : JsonWebSocket
    {
        public AgentWebSocket(string uri)
            : base(uri)
        {
        }

        protected override string SerializeObject(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        protected override object DeserializeObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
    }

    public enum WebSocketClientState
    {
        Offline,
        Connecting,
        Connected
    }

    public enum LoginState
    {
        TimedOut = -2,
        Failed = -1,
        Idle = 0,
        InProgress = 1,
        Success = 2
    }


    public enum LoginInformation
    {
        TimedOut = -2,
        Failed = -1,
        Idle = 0,
        InProgress = 1,
        Success = 2
    }

    public class WebSocketClient
    {
        public static int TimeOut = 30;
        private RemoteUser _userInformation;
        private string _errorMessage;
        private WebSocketClientState _state = WebSocketClientState.Offline;
        private LoginState _loginState = LoginState.Idle;
        private string _loginErrorMsg;


        private string _uri;

        public WebSocketClient(string uri)
        {
            State = WebSocketClientState.Connecting;
            InitializeWebSocket(uri);
        }

        public AgentWebSocket AgentWebSocket { get; private set; }


        public RemoteUser UserInformation
        {
            get => _userInformation;
            set
            {
                _userInformation = value;
                RaisePropertyChanged("UserInformation");
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public WebSocketClientState State
        {
            get => _state;
            set
            {
                _state = value;
                _loginState = LoginState.Idle;
                RaisePropertyChanged("State");
            }
        }

        private void InitializeWebSocket(string uri)
        {
            _uri = uri;
            try
            {
                AgentWebSocket = new AgentWebSocket(_uri);
            }
            catch (Exception)
            {
                State = WebSocketClientState.Offline;
                ErrorMessage = $"Invalid server URI ({_uri})!";
                throw new Exception($"Invalid server URI ({_uri})!");
            }

            AgentWebSocket.Security.AllowUnstrustedCertificate = true;
            AgentWebSocket.Closed += WebSocket_Closed;
            AgentWebSocket.Error += WebSocket_Error;
            AgentWebSocket.Opened += WebSocket_Opened;
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            State = WebSocketClientState.Connected;
        }

        public void StartConnect(bool requireLogin, string email = null, string password = null)
        {
            if (_state  == WebSocketClientState.Connected && !requireLogin)
            {
                return;
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                if (AgentWebSocket == null)
                    throw new Exception("StartConnect() AgentWebSocket == null");

                if (AgentWebSocket.State == WebSocketState.Closed)
                    InitializeWebSocket(_uri);

                State = WebSocketClientState.Connecting;

                AgentWebSocket.Open();

                var starttime = DateTime.UtcNow;
                while (State != WebSocketClientState.Connected &&
                       State != WebSocketClientState.Offline)
                {
                    Application.DoEvents();
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff < TimeOut) continue;

                    if (State != WebSocketClientState.Offline)
                        AgentWebSocket.Close();

                    throw new Exception($"StartConnect() Server connection timeout (total send time = {diff})!");
                }
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                throw new Exception($"StartConnect() Server Offline! (client state = {Program.WebSocketClient.State})");
            }

            if(requireLogin)
            {
                StartLogin(email, password);
            }

        }

        public void StartLogin(string email, string password)
        {

            switch (_loginState)
            {
                case LoginState.Success:
                    return;
                case LoginState.InProgress:
                    throw new Exception(@"Logged-in already in progress!");
                case LoginState.TimedOut:
                case LoginState.Failed:
                case LoginState.Idle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _loginState = LoginState.InProgress;

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
            dynamic loginInfo = new ExpandoObject();
            loginInfo.Mail = email;
            loginInfo.Password = password;
            loginInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

            AgentWebSocket.Query<dynamic>("LOGIN", (object)loginInfo, OnLoggedIn);

            var starttime = DateTime.UtcNow;
            while (_loginState == LoginState.InProgress && State == WebSocketClientState.Connected)
            {
                Application.DoEvents();
                var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                if (diff < TimeOut) continue;

                if (State != WebSocketClientState.Offline)
                    AgentWebSocket.Close();

                throw new Exception($"StartLogin() Server response timeout (total send time = {diff})!");
            }

            if (_loginState == LoginState.Success) return;

            var msg = !string.IsNullOrEmpty(_loginErrorMsg) ? _loginErrorMsg : ErrorMessage;
            throw new Exception(msg);
        }


        private void OnLoggedIn(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                UserInformation = result["RemoteUser"].ToObject<RemoteUser>();
                _loginState = LoginState.Success;
            }
            else
            {

                _loginErrorMsg = result["Message"].ToObject<string>();
                ErrorMessage = _loginErrorMsg;
                _loginState = LoginState.Failed;
            }
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            State = WebSocketClientState.Offline;

            if (string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = "Offline";
        }

        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            if (e.Exception == null) return;

            if (e.Exception is SocketException exception && exception.ErrorCode == (int)SocketError.AccessDenied)
                ErrorMessage = new SocketException((int)SocketError.ConnectionRefused).Message;
            else
                ErrorMessage = e.Exception.StackTrace;

            if (AgentWebSocket.State != WebSocketState.None ||
                State != WebSocketClientState.Connecting) return;
            
            State = WebSocketClientState.Offline;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}