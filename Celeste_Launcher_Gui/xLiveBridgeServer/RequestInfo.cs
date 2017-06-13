#region Using directives

using System;
using System.IO;
using SuperSocket.SocketBase.Protocol;

#endregion

namespace Celeste_Launcher_Gui.xLiveBridgeServer
{
    public class RequestInfo : IRequestInfo
    {
        public RequestInfo(PacketType packetType, byte[] packet)
        {
            PacketType = packetType;
            Packet = packet;
        }

        public PacketType PacketType { get; }
        public byte[] Packet { get; }

        public int RequestInfoLength => 5 + Packet.Length;
        public string Key => Enum.GetName(typeof(PacketType), PacketType);

        public override string ToString()
        {
            const string formatString =
                "\r\n" +
                "           PacketType = {0}\r\n" +
                "           Packet = {1}";

            return string.Format(formatString, PacketType, Packet);
        }

        public static RequestInfo FromByteArray(byte[] data, int offset, int length)
        {
            using (var ms = new MemoryStream(data, offset, length))
            {
                using (var br = new BinaryReader(ms))
                {
                    var packetType = (PacketType) br.ReadByte();
                    if (length <= 1) return new RequestInfo(packetType, new byte[0]);

                    var packet = br.ReadBytes(length - 1);
                    return new RequestInfo(packetType, packet);
                }
            }
        }
    }
}