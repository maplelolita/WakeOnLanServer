using System.Globalization;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WolServer
{
    public static class WolCore
    {
        public static async Task Wake(string host, ushort port, string mac)
        {
            var macAddr = ParseMacAddr(mac);
            await SendRequest(host, port, macAddr);
        }

        public static byte[] ParseMacAddr(string mac)
        {
            mac = mac.Trim().Replace("-", "").Replace(":", "");
            byte[] bytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                var bstring = mac.Substring(i * 2, 2);
                var b       = byte.Parse(bstring, NumberStyles.AllowHexSpecifier);
                bytes[i] = b;
            }
            return bytes;
        }


        public static byte[] BuildMagicPacket(byte[] macAddr)
        {
            var packet = new byte[6 + 6 * 16];
            for (var i = 0; i < 6; i++)
            {
                packet[i] = 0xff;
            }
            for (var i = 0; i < 16; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    packet[6 + i * 6 + j] = macAddr[j];
                }
            }
            return packet;
        }

        public static Task SendRequest(string host, ushort port, byte[] macAddr)
        {
            using (var client = new UdpClient())
            {
                var packet = BuildMagicPacket(macAddr);
                return client.SendAsync(packet, packet.Length, host, port);
            }
        }

    }
}
