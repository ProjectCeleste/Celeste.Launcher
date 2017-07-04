#region Using directives

using System;
using System.ComponentModel;
using System.Net.Sockets;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using WebSocket4Net;

#endregion

namespace Celeste_Launcher_Gui
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
        Connected,
        Logging,
        Logged
    }

    public class WebSocketClient
    {
        private string _errorMessage;
        private WebSocketClientState _state = WebSocketClientState.Offline;

        private string _uri;

        public WebSocketClient(string uri)
        {
            State = WebSocketClientState.Connecting;
            InitializeWebSocket(uri);
        }

        public AgentWebSocket AgentWebSocket { get; private set; }


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
                ErrorMessage = $"Invalid server URI ({_uri})!";
                State = WebSocketClientState.Offline;
                return;
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

        public void StartConnect()
        {
            if (AgentWebSocket == null)
                return;

            if (AgentWebSocket.State == WebSocketState.Closed)
                InitializeWebSocket(_uri);

            State = WebSocketClientState.Connecting;

            AgentWebSocket.Open();
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
            var exception = e.Exception as SocketException;

            if (exception != null && exception.ErrorCode == (int) SocketError.AccessDenied)
                ErrorMessage = new SocketException((int) SocketError.ConnectionRefused).Message;
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