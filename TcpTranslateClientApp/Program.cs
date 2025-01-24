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
using StreamWriter writer = new StreamWriter(stream);
using StreamReader reader = new StreamReader(stream);

using BinaryWriter binaryWriter = new BinaryWriter(stream);

//List<byte> data = new();
//int byteRead = 0;

while (true)
{
    Console.Write("Input word: ");
    string? word = Console.ReadLine();

    //byte[] bufferWord = Encoding.UTF8.GetBytes(word + '\n');
    //await stream.WriteAsync(bufferWord);

    //while((byteRead = stream.ReadByte()) != stopChar)
    //{
    //    data.Add((byte)byteRead);
    //}
    //string translateWord = Encoding.UTF8.GetString(data.ToArray());

    await writer.WriteLineAsync(word);
    await writer.FlushAsync();

    if (word == "file")
    {
        using FileStream fileStream = File.Open("D:\\ada.jpg", FileMode.Open);
        long size = fileStream.Length;
        binaryWriter.Write(size);
        binaryWriter.Flush();
        Console.WriteLine($"Send size {size} of file");

        byte[] buffer = new byte[size];
        fileStream.Read(buffer);
        binaryWriter.Write(buffer);
        binaryWriter.Flush();
        Console.WriteLine("Send file");
        continue;
    }
    
    string? translateWord = await reader.ReadLineAsync();

    Console.WriteLine($"Word: {word} | Translate: {translateWord}");

    //data.Clear();
}
