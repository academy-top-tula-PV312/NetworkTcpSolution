// CLIENT

using System.Net;
using System.Net.Sockets;
using System.Text;

using Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    await clientSocket.ConnectAsync(new IPEndPoint(IPAddress.Loopback, 5000));
    Console.WriteLine($"Connect to server {clientSocket.RemoteEndPoint}");

    byte[] buffer = new byte[512];

    int bytesRead = await clientSocket.ReceiveAsync(buffer);
    string message = Encoding.UTF8.GetString(buffer);

    Console.WriteLine($"Server sended message: {message}");

    message = "Hello server";
    buffer = Encoding.UTF8.GetBytes(message);

    await clientSocket.SendAsync(buffer);
    Console.WriteLine("Client send message to server");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}