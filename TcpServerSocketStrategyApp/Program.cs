// SERVER PROJECT STRATEGY

using System.Net;
using System.Net.Sockets;
using System.Text;

using Socket serverSocket = new(AddressFamily.InterNetwork, 
                                SocketType.Stream, 
                                ProtocolType.Tcp);

var ip = IPAddress.Loopback;
var port = 5000;
char stopChar = '\n';

try
{
    serverSocket.Bind(new IPEndPoint(ip, port));
    serverSocket.Listen();
    Console.WriteLine("Server starting");

    while (true)
    {
        using Socket clientSocket = await serverSocket.AcceptAsync();

        byte[] bufferSize = new byte[4];
        await clientSocket.ReceiveAsync(bufferSize);

        int messageSize = BitConverter.ToInt32(bufferSize, 0);

        byte[] bufferData = new byte[messageSize];
        int bytesRead = await clientSocket.ReceiveAsync(bufferData);

        string message = Encoding.UTF8.GetString(bufferData);
        Console.WriteLine($"[Size {messageSize}] || Message: {message}");
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    serverSocket.Close();
}


async Task ReadWithStopChar()
{
    try
    {
        serverSocket.Bind(new IPEndPoint(ip, port));
        serverSocket.Listen();
        Console.WriteLine("Server starting");

        while (true)
        {
            using Socket clientSocket = await serverSocket.AcceptAsync();

            List<byte> data = new List<byte>();

            int count = 0;

            while (true)
            {
                count = clientSocket.ReadByte(); //await clientSocket.ReceiveAsync(buffer);
                if (count == -1 || count == stopChar) break; // stop read
                data.Add((byte)count);
            }

            string message = Encoding.UTF8.GetString(data.ToArray());
            Console.WriteLine($"Client {clientSocket.RemoteEndPoint} message: {message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        serverSocket.Close();
    }
}

public static class SocketExtension
{
    public static int ReadByte(this Socket socket)
    {
        byte b = 0;
        Span<byte> buffer = new Span<byte>(ref b);
        int count = socket.Receive(buffer);
        return count == 0 ? -1 : b;
    }
}