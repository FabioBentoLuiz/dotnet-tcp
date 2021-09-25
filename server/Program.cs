using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new TcpServer();
            await server.Run();
        }
    }
}
