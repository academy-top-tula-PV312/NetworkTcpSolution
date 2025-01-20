// CLIENT Translate

using System.Net;
using System.Net.Sockets;
using System.Text;

char stopChar = '\n';

IPAddress ip = IPAddress.Loopback;
int port = 5000;

TcpClient client = new();
await client.ConnectAsync(ip, port);
NetworkStream stream = client.GetStream();

List<byte> data = new();
int byteRead = 0;

while(true)
{
    Console.Write("Input word: ");
    string? word = Console.ReadLine();

    byte[] bufferWord = Encoding.UTF8.GetBytes(word + '\n');
    await stream.WriteAsync(bufferWord);

    while((byteRead = stream.ReadByte()) != stopChar)
    {
        data.Add((byte)byteRead);
    }

    string translateWord = Encoding.UTF8.GetString(data.ToArray());
    Console.WriteLine($"Word: {word} | Translate: {translateWord}");

    data.Clear();
}
