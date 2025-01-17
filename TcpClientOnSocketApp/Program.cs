// CLIENT

using System.Net;
using System.Net.Sockets;

using Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    await clientSocket.ConnectAsync(new IPEndPoint(IPAddress.Loopback, 5000));
    Console.WriteLine($"Connect to server {clientSocket.RemoteEndPoint}");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}