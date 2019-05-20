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
        public const int TimeOut = 30; //30 Seconds

        private readonly string _uri;

        private Agent _agent;

        private string _errorMessage;

        private ClientState _state = ClientState.Offline;

        public Client(string uri)
        {
            _uri = uri;
        }

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
                _agent = new Agent(_uri);
            }
            catch (Exception ex)
            {
                throw new Exception($"Invalid server URI ({_uri}).Exception: {ex.Message}");
            }

#if DEBUG
            if (_uri.StartsWith("wss://", StringComparison.OrdinalIgnoreCase))
            {
                _agent.Security.EnabledSslProtocols = SslProtocols.Tls;

                _agent.Security.AllowUnstrustedCertificate = true;
                _agent.Security.AllowNameMismatchCertificate = true;
                _agent.Security.AllowCertificateChainErrors = true;
            }
#endif

            _agent.Closed += WebSocket_Closed;
            _agent.Error += WebSocket_Error;
            _agent.Opened += WebSocket_Opened;
        }

        public string Query<T>(string name, object content, Action<T> executor)
        {
            return _agent.Query(name, content, executor);
        }

        public void Disconnect()
        {
            try
            {
                _agent.Close();
            }
            catch (Exception)
            {
                //
            }
        }

        public async Task DoConnect()
        {
            if (_state == ClientState.Connected)
                return;

            InitializeWebSocket();

            State = ClientState.Connecting;

            _agent.Open();

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

            if (_agent.State != WebSocketState.None ||
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