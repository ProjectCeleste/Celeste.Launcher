#region Using directives

using System.Dynamic;
using System.IO;
using System.Text;
using Celeste_Launcher_Gui.Helpers;
using SuperSocket.SocketBase.Command;

#endregion

namespace Celeste_Launcher_Gui.xLiveBridgeServer.Command
{
    public class XUserFindUsers : CommandBase<Session, RequestInfo>
    {
        public override void ExecuteCommand(Session session, RequestInfo requestInfo)
        {
            string userName;
            using (var ms = new MemoryStream(requestInfo.Packet))
            {
                using (var br = new BinaryReader(ms))
                {
                    var str0 = Encoding.Default.GetString(br.ReadBytes(requestInfo.Packet.Length));
                    var str = str0.Split('\0');
                    userName = str[0];
                }
            }

            if (!string.IsNullOrEmpty(userName)) DoXUserFindUsers(userName);
        }

        public void DoXUserFindUsers(string userName)
        {
            if (Program.WebSocketClient.State != WebSocketClientState.Logged) return;

            dynamic xUserFindUsersInfo = new ExpandoObject();
            xUserFindUsersInfo.UserName = userName;

            Program.WebSocketClient.AgentWebSocket?.Query<dynamic>("XUSERFINDUSERS", (object) xUserFindUsersInfo,
                OnXUserFindUsers);
        }

        private static void OnXUserFindUsers(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
                using (var ms = new MemoryStream())
                {
                    using (var bw = new BinaryWriter(ms))
                    {
                        var xuid = result["Xuid"].ToObject<long>();

                        var output = Encoding.Default.GetBytes($"xuid = {xuid:X}\r\n");
                        bw.Write(output);

                        var data = ms.ToArray();
                        foreach (var session in Program.Server.GetAllSessions())
                            session.Send(data, 0, data.Length);
                    }
                }
            else
                using (var ms = new MemoryStream())
                {
                    using (var bw = new BinaryWriter(ms))
                    {
                        var xuid = result["Xuid"].ToObject<long>();

                        var output = Encoding.Default.GetBytes($"xuid = {xuid:X}\r\n");
                        bw.Write(output);

                        var data = ms.ToArray();
                        foreach (var session in Program.Server.GetAllSessions())
                            session.Send(data, 0, data.Length);
                    }
                }
        }
    }
}