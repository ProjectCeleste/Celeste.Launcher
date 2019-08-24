#region Using directives

using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading.Tasks;
using Celeste_Public_Api.Logging;
using Celeste_Public_Api.WebSocket_Api.WebSocket.Enum;
using Serilog;
using SuperSocket.ClientEngine;
using WebSocket4Net;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket
{
    public class Client
    {
        public const int ConnectionTimeoutInSeconds = 30; //30 Seconds

        private readonly string _uri;

        private Agent _agent;

        private string _errorMessage;

        private ClientState _state = ClientState.Offline;

        private ILogger _logger;

        public Client(string uri)
        {
            _uri = uri;
            _logger = LoggerFactory.GetLogger();
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

//#if DEBUG
            if (_uri.StartsWith("wss://", StringComparison.OrdinalIgnoreCase))
            {
                _agent.Security.EnabledSslProtocols = SslProtocols.Tls;

                _agent.Security.AllowUnstrustedCertificate = true;
                _agent.Security.AllowNameMismatchCertificate = true;
                _agent.Security.AllowCertificateChainErrors = true;
            }
//#endif

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
            {
                _logger.Debug("Socket already connected");
                return;
            }

            _logger.Debug("Initializing web socket");
            InitializeWebSocket();

            State = ClientState.Connecting;

            _logger.Debug("Opening web socket");
            _agent.Open();

            var startTime = DateTime.UtcNow;
            while (State == ClientState.Connecting)
            {
                await Task.Delay(10);
                _logger.Debug("Performing spin wait");

                var timeSpentInSeconds = DateTime.UtcNow.Subtract(startTime).TotalSeconds;
                if (timeSpentInSeconds > ConnectionTimeoutInSeconds)
                {
                    State = ClientState.TimeOut;
                    _logger.Debug("Timing out");

                    throw new Exception($"Server connection timeout ({timeSpentInSeconds}s)!");
                }
            }

            if (State != ClientState.Connected)
                throw new Exception("Server Offline!");

            await Task.Delay(200).ConfigureAwait(false);
            _logger.Debug("Connected");
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