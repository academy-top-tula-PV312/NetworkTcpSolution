// TCP CLIENT

using System.Net;
using System.Net.Sockets;

using TcpClient client = new();

try
{
    await client.ConnectAsync(IPAddress.Loopback, 5000);
    Console.WriteLine($"connected to server {client.Client.RemoteEndPoint}");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}