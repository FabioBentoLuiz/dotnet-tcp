using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{


    class TcpServer
    {
        public async Task Run()
        {
            var server = CreateServer();

            var hasLoggedRequest = false;

            while (true)
            {
                if (server.Pending())
                {
                    await ProccessMessageReceived(server);
                }
                else
                {
                    if (!hasLoggedRequest)
                    {
                        Console.WriteLine("No pending requests.");
                        Console.WriteLine("Server listening...");
                        hasLoggedRequest = true;
                    }
                }
            }
        }

        private async Task ProccessMessageReceived(TcpListener server)
        {
            using (var client = await server.AcceptTcpClientAsync())
            {
                using (var tcpStream = client.GetStream())
                {
                    var buffer = new byte[1024];
                    await tcpStream.ReadAsync(buffer, 0, buffer.Length);
                    var messageReceived = Encoding.UTF8.GetString(buffer).Replace("\0", string.Empty);
                    Console.WriteLine($"Message received: {messageReceived}");

                    var responseMessage = $"{messageReceived}  from the server";
                    var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                    await tcpStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
        }

        private TcpListener CreateServer()
        {
            var port = 3000;
            var ip = IPAddress.Any;
            var server = new TcpListener(ip, port);
            server.Start();
            return server;
        }
    }
}
