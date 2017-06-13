#region Using directives

using System.IO;
using System.Text;
using SuperSocket.SocketBase.Command;

#endregion

namespace Celeste_Launcher_Gui.xLiveBridgeServer.Command
{
    public class GetUserInfo : CommandBase<Session, RequestInfo>
    {
        public override void ExecuteCommand(Session session, RequestInfo requestInfo)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    var signinInfo = $"user name = {Program.RemoteUser.ProfileName}\r\n" +
                                     $"xuid = {Program.RemoteUser.Xuid:X}\r\n" +
                                     $"server ip = {Program.RemoteUser.ServerIp}\r\n" +
                                     $"auth token = {Program.RemoteUser.AuthToken}\r\n";

                    var output = Encoding.Default.GetBytes(signinInfo);
                    bw.Write(output);

                    var data = ms.ToArray();
                    session.Send(data, 0, data.Length);
                }
            }
        }
    }
}