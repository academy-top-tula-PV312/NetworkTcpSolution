// TCP LISTENER (SERVER)

using System.Net;
using System.Net.Sockets;

TcpListener server = new(IPAddress.Loopback, 5000);

try
{
    server.Start();
    Console.WriteLine("Server starting...");

    while(true)
    {
        using TcpClient client = await server.AcceptTcpClientAsync();
        Console.WriteLine($"Client {client.Client.RemoteEndPoint} accepting");
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    server.Stop();
}