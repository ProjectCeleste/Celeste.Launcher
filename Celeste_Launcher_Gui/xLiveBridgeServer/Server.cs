#region Using directives

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

#endregion

namespace Celeste_Launcher_Gui.xLiveBridgeServer
{
    public class Server : AppServer<Session, RequestInfo>
    {
        public Server() : base(new DefaultReceiveFilterFactory<ReceiveFilter, RequestInfo>())
        {
        }
    }
}