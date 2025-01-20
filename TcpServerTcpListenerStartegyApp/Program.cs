// SERVER TcpListener Strategy

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress ip = IPAddress.Loopback;
int port = 5000;
char stopChar = '\n';

TcpListener server = new(ip, port);

try
{
    server.Start();
    Console.WriteLine("Server starting");

    while(true)
    {
        using TcpClient client = await server.AcceptTcpClientAsync();
        NetworkStream stream = client.GetStream();

        //await SimpleWriteReadMessage(stream);
        //await ReadWithStopChar(stream);

        byte[] bufferSize = new byte[4];
        await stream.ReadExactlyAsync(bufferSize, 0, bufferSize.Length);
        int messageSize = BitConverter.ToInt32(bufferSize, 0);

        byte[] bufferMessage = new byte[messageSize];
        int bytes = await stream.ReadAsync(bufferMessage);

        string message = Encoding.UTF8.GetString(bufferMessage);

        Console.WriteLine($"[Size: {messageSize}] || Message: {message}");
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


async Task SimpleWriteReadMessage(NetworkStream stream)
{
    string message = $"Connect. Data: {DateTime.Now.ToLongDateString()}. Time: {DateTime.Now.ToShortTimeString()}";
    byte[] buffer = Encoding.UTF8.GetBytes(message);

    await stream.WriteAsync(buffer, 0, buffer.Length);
    Console.WriteLine("Send message to client");

    StringBuilder stringBuilder = new();
    int bytes = 0;

    do
    {
        bytes = await stream.ReadAsync(buffer);
        stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
    } while (bytes > 0);

    Console.WriteLine($"Client message: {stringBuilder.ToString()}");
}

async Task ReadWithStopChar(NetworkStream stream)
{
    List<byte> data = new List<byte>();
    int bytesRead = stopChar;

    while ((bytesRead = stream.ReadByte()) != stopChar)
    {
        data.Add((byte)bytesRead);
    }

    string message = Encoding.UTF8.GetString(data.ToArray());
    Console.WriteLine($"Client message: {message}");
}