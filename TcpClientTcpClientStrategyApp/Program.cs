// CLIENT TcpClient Strategy

using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress ip = IPAddress.Loopback;
int port = 5000;

using TcpClient client = new();
client.Connect(ip, port);
NetworkStream stream = client.GetStream();

Console.Write("Input message: ");
string? message = Console.ReadLine();
byte[] buffer = Encoding.UTF8.GetBytes(message + '\n' + message);

byte[] size = BitConverter.GetBytes(buffer.Length);

await stream.WriteAsync(size, 0, size.Length);
await stream.WriteAsync(buffer, 0, buffer.Length);

Console.WriteLine("Send nessage to server");



async Task SimpleReadWriteMessage()
{
    byte[] buffer = new byte[1024];

    int bytes = await stream.ReadAsync(buffer);

    string message = Encoding.UTF8.GetString(buffer, 0, bytes);
    Console.WriteLine($"Message from server: {message}");

    message = "Client message. Hello server!";
    buffer = Encoding.UTF8.GetBytes(message);

    await stream.WriteAsync(buffer);
    Console.WriteLine("Send nessage to server");
}

async Task WriteWithStopChar()
{
    Console.Write("Input message: ");
    string? message = Console.ReadLine();
    byte[] buffer = Encoding.UTF8.GetBytes(message + '\n');

    await stream.WriteAsync(buffer, 0, buffer.Length);
    Console.WriteLine("Message sended to server");
}