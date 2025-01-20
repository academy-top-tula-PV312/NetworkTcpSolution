// CLIENT PROJECT STRATEGY

using System.Net.Sockets;
using System.Net;
using System.Text;

using Socket clientSocket = new(AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp);

var ip = IPAddress.Loopback;
var port = 5000;

try
{
    await clientSocket.ConnectAsync(ip, port);
    Console.Write("Input message: ");
    string? message = Console.ReadLine();

    // Strategy stop char
    //byte[] buffer = Encoding.UTF8.GetBytes(message + "\n");
    //await clientSocket.SendAsync(buffer);

    // strategy message size
    byte[] buffer = Encoding.UTF8.GetBytes(message + "\n" + message);
    byte[] size = BitConverter.GetBytes(buffer.Length);

    await clientSocket.SendAsync(size);
    await clientSocket.SendAsync(buffer);

    Console.WriteLine("Message sended to server");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}