// SERVER

using System.Net;
using System.Net.Sockets;

IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 5000);

Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

serverSocket.Bind(endPoint);

serverSocket.Listen();
Console.WriteLine("Server starting");

using Socket clientSocket = await serverSocket.AcceptAsync();

Console.WriteLine($"Remote client {clientSocket.RemoteEndPoint}");
