// SERVER

using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 5000);
Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    serverSocket.Bind(endPoint);
    serverSocket.Listen();
    Console.WriteLine("Server starting");

    while (true)
    {
        using Socket clientSocket = await serverSocket.AcceptAsync();
        Console.WriteLine($"Remote client {clientSocket.RemoteEndPoint}");

        string message = "Hello client";
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await clientSocket.SendAsync(buffer);

        //int bytesRead = await clientSocket.ReceiveAsync(buffer);
        //message = Encoding.UTF8.GetString(buffer);
        //Console.WriteLine($"Client sended message: {message}");

        List<byte> data = new List<byte>();
        int bytes = 0;

        do
        {
            bytes = await clientSocket.ReceiveAsync(buffer);
            data.AddRange(buffer.Take(bytes));
        } while(bytes > 0);

        message = Encoding.UTF8.GetString(data.ToArray());
        Console.WriteLine($"Client sended message: {message}");
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}




