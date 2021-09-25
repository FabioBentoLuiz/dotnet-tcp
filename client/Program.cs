using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var port = 3000;
            var ip = IPAddress.Parse("127.0.0.1");
            using (var client = new TcpClient())
            {
                client.Connect(ip, port);
                if (client.Connected)
                {

                    var message = "Hi";
                    var buffer = Encoding.UTF8.GetBytes(message);
                    using (var requestStream = client.GetStream())
                    {
                        await requestStream.WriteAsync(buffer, 0, buffer.Length);
                        var responseBytes = new byte[1024];
                        await requestStream.ReadAsync(responseBytes, 0, responseBytes.Length);
                        var responseMessage = Encoding.UTF8.GetString(responseBytes);
                        Console.WriteLine($"Server response: {responseMessage}");
                    }
                }
            }

        }
    }
}
