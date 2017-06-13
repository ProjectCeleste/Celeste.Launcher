#region Using directives

using SuperSocket.SocketBase;

#endregion

namespace Celeste_Launcher_Gui.xLiveBridgeServer
{
    public class Session : AppSession<Session, RequestInfo>
    {
        public new Server AppServer => (Server) base.AppServer;
    }
}