using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Helpers
{
    public static class HostHeplers
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString(); // Return the first IPV4 address found
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        // Build the URL with the detected IP address
        public static string ipAddress = GetLocalIPAddress();
        public static string port = "5001"; // Or use a configurable port
        public static string url = $"https://{ipAddress}:{port}";
    }
}
