// SERVER Translate

using System.Net;
using System.Net.Sockets;
using System.Text;

Dictionary<string, string> words = new()
{
    { "table", "стол" },
    { "color", "цвет" },
    { "month", "месяц" },
    { "house", "дом" },
    { "cat", "кошка" },
};

string stopWord = "end";
char stopChar = '\n';

IPAddress ip = IPAddress.Loopback;
int port = 5000;

TcpListener server = new(ip, port);

try
{
    server.Start();
    Console.WriteLine("Server starting");

    while(true) // listen and accept clients
    {
        using TcpClient client = await server.AcceptTcpClientAsync();
        Console.WriteLine($"Accept client {client.Client.RemoteEndPoint}");

        NetworkStream stream = client.GetStream();
        List<byte> data = new();

        int byteRead = 0;

        while(true) // dialog with client
        {
            while((byteRead = stream.ReadByte()) != stopChar)
            {
                data.Add((byte)byteRead);
            }

            string word = Encoding.UTF8.GetString(data.ToArray());

            if (word == stopWord)
                break;

            Console.WriteLine($"Client's word: {word}");
            
            string? answer = words.GetValueOrDefault(word);
            if (answer == null) answer = "Word not found";

            answer += stopChar;

            await stream.WriteAsync(Encoding.UTF8.GetBytes(answer));
            data.Clear();
        }
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

