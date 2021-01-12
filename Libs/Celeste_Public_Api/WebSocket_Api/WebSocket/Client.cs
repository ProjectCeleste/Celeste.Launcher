#region Using directives

using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
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
        public const int ConnectionTimeoutInMilliseconds = 30 * 1000;

        private readonly string _uri;

        private Agent _agent;
        private readonly ILogger _logger;

        private SemaphoreSlim _socketCallbackSemaphore;

        public Client(string uri)
        {
            _uri = uri;
            _logger = LoggerFactory.GetLogger();
        }

        public string ErrorMessage { get; private set; }

        public ClientState State { get; private set; } = ClientState.Offline;

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

            _agent.Closed += WebSocket_Closed;
            _agent.Error += WebSocket_Error;
            _agent.Opened += WebSocket_Opened;

            _socketCallbackSemaphore?.Dispose();
            _socketCallbackSemaphore = new SemaphoreSlim(0, 1);
        }

        public string Query<T>(string name, object content, Action<T> executor)
        {
            return _agent.Query(name, content, executor);
        }

        public void Disconnect()
        {
            try
            {
                _agent?.Close();
            }
            catch (Exception)
            {
                //
            }
        }

        public async Task DoConnect()
        {
            if (State == ClientState.Connected)
            {
                _logger.Debug("Socket already connected");
                return;
            }

            _logger.Debug("Initializing web socket");
            InitializeWebSocket();

            State = ClientState.Connecting;

            _logger.Information("Attempting to connect to {@Uri}", _uri);
            _agent.Open();

            var receivedSocketCallback = await _socketCallbackSemaphore.WaitAsync(ConnectionTimeoutInMilliseconds);

            if (!receivedSocketCallback)
            {
                State = ClientState.TimeOut;
                _logger.Warning("Timed out connecting to server");

                throw new Exception($"Server connection timeout)!");
            }

            if (State != ClientState.Connected)
                throw new Exception("Server Offline!");

            _logger.Information("Successfully connected websocket");
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            _logger.Debug("Web socket open");
            State = ClientState.Connected;
            _socketCallbackSemaphore.Release();
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            _logger.Debug("Web socket closed");
            State = ClientState.Offline;

            if (string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = "Offline";

            _socketCallbackSemaphore.Release();
        }

        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            if (e.Exception == null) return;

            if (e.Exception is SocketException exception && exception.ErrorCode == (int) SocketError.AccessDenied)
                ErrorMessage = new SocketException((int) SocketError.ConnectionRefused).Message;
            else
                ErrorMessage = e.Exception.StackTrace;

            _logger.Error(e.Exception, e.Exception.Message);

            if (_agent.State != WebSocketState.None ||
                State != ClientState.Connecting) return;

            State = ClientState.Offline;
            _socketCallbackSemaphore.Release();
        }
    }
}