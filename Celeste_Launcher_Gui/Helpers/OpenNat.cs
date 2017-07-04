#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public class OpenNat
    {
        public static TextWriterTraceListener TextWriterTraceListener =
            new TextWriterTraceListener($"{AppDomain.CurrentDomain.BaseDirectory}OpenNat.log", "OpenNatTxtLog");

        public static async Task<int> MapPortTask(int privatePort, int publicPort)
        {
            try
            {
                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}OpenNat.log"))
                    File.Delete($"{AppDomain.CurrentDomain.BaseDirectory}OpenNat.log");
            }
            catch
            {
                // ignored
            }

            NatDiscoverer.TraceSource.Switch.Level = SourceLevels.All;
            if (!NatDiscoverer.TraceSource.Listeners.Contains(TextWriterTraceListener))
            NatDiscoverer.TraceSource.Listeners.Add(TextWriterTraceListener);

            var nat = new NatDiscoverer();

            var cts = new CancellationTokenSource(15 * 1000); //15 Sec
            var device = await nat.DiscoverDeviceAsync(PortMapper.Upnp, cts);
            var externalIp = await device.GetExternalIPAsync();
            var allMappings = await device.GetAllMappingsAsync();

            var localIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .FirstOrDefault(key => key.AddressFamily == AddressFamily.InterNetwork);

            var enumerable = allMappings as Mapping[] ?? allMappings.ToArray();
            if (enumerable.Any(key => Equals(key.PrivateIP, localIp) &&
                                      key.Description == "AOEO Project Celeste"))
            {
                var r = enumerable.FirstOrDefault(key => Equals(key.PrivateIP, localIp) &&
                                                         key.Description == "AOEO Project Celeste");
                if (r != null)
                    await device.DeletePortMapAsync(r);
            }

            var rnd = new Random(DateTime.UtcNow.Millisecond);
            var newPublicPort = publicPort;
            while (enumerable.Any(key => key.PublicPort == newPublicPort))
                newPublicPort = rnd.Next(1000, ushort.MaxValue);

            await device.CreatePortMapAsync(new Mapping(Protocol.Udp, privatePort, newPublicPort, int.MaxValue,
                "AOEO Project Celeste"));

            allMappings = await device.GetAllMappingsAsync();
            enumerable = allMappings as Mapping[] ?? allMappings.ToArray();

            var sb = new StringBuilder();
            sb.AppendFormat(
                "\n+------+-------------------------------+--------------------------------+------------------------------------+-------------------------+");
            sb.AppendFormat(
                "\n| PROT | PUBLIC (Reacheable)		   | PRIVATE (Your computer)		| Description						|						 |");
            sb.AppendFormat(
                "\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
            sb.AppendFormat("\n|	  | IP Address		   | Port   | IP Address			| Port   |									| Expires				 |");
            sb.AppendFormat(
                "\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");
            foreach (var mapping in enumerable)
                sb.AppendFormat("\n|  {5} | {0,-20} | {1,6} | {2,-21} | {3,6} | {4,-35}|{6,25}|",
                    externalIp, mapping.PublicPort, mapping.PrivateIP, mapping.PrivatePort, mapping.Description,
                    mapping.Protocol == Protocol.Tcp ? "TCP" : "UDP", mapping.Expiration.ToLocalTime());
            sb.AppendFormat(
                "\n+------+----------------------+--------+-----------------------+--------+------------------------------------+-------------------------+");

            NatDiscoverer.TraceSource.TraceInformation(sb.ToString());

            if (enumerable.Any(key => Equals(key.PrivateIP, localIp) &&
                                      key.PublicPort == newPublicPort
                                      && key.PrivatePort == privatePort && key.Description == "AOEO Project Celeste"))
                return newPublicPort;

            throw new Exception("Port mapping fail!");
        }
    }
}