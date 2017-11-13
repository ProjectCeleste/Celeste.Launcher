#region Using directives

using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Enum;
using SuperSocket.ClientEngine;
using WebSocket4Net;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket
{
    public class Client
    {
        public const int TimeOut = 30;

        private string _errorMessage;

        private ClientState _state = ClientState.Offline;

        private string _uri;

        internal Client(string uri)
        {
            _uri = uri;
            State = ClientState.Connecting;
            InitializeWebSocket();
        }

        public Agent Agent { get; private set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set
            {
                _errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public ClientState State
        {
            get => _state;
            private set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InitializeWebSocket()
        {
            try
            {
                Agent = new Agent(_uri);
            }
            catch (Exception)
            {
                throw new Exception($"Invalid server URI ({_uri})!");
            }

            Agent.Security.AllowUnstrustedCertificate = true;

            Agent.Closed += WebSocket_Closed;
            Agent.Error += WebSocket_Error;
            Agent.Opened += WebSocket_Opened;
        }

        public async Task DoConnect()
        {
            if (_state == ClientState.Connected)
                return;

            if (Agent == null || Agent.State == WebSocketState.Closed)
                InitializeWebSocket();

            State = ClientState.Connecting;

            Agent.Open();

            var starttime = DateTime.UtcNow;
            while (State == ClientState.Connecting)
            {
                var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                if (diff < TimeOut)
                    continue;

                State = ClientState.TimeOut;

                throw new Exception($"Server connection timeout ({diff}s)!");
            }

            if (State != ClientState.Connected)
                throw new Exception("Server Offline!");

            await Task.Delay(200).ConfigureAwait(false);
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            State = ClientState.Connected;
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            State = ClientState.Offline;

            if (string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = "Offline";
        }

        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            if (e.Exception == null) return;

            if (e.Exception is SocketException exception && exception.ErrorCode == (int) SocketError.AccessDenied)
                ErrorMessage = new SocketException((int) SocketError.ConnectionRefused).Message;
            else
                ErrorMessage = e.Exception.StackTrace;

            if (Agent.State != WebSocketState.None ||
                State != ClientState.Connecting) return;

            State = ClientState.Offline;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}