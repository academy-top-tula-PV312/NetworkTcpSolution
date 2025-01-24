// CLIENT TCP CHAT

using System.Net;
using System.Net.Sockets;

Console.Write("Input server's ip: ");
string? ip = Console.ReadLine();
int port = 5000;

using TcpClient client = new();
StreamReader? reader = null;
StreamWriter? writer = null;
string? name;

Console.Write("Input name: ");
name = Console.ReadLine();

try
{
    client.Connect(ip, port);
    reader = new(client.GetStream());
    writer = new(client.GetStream());

    if (reader is null || writer is null) return;

    Task.Run(() => ReceiveMessageAsync(reader));
    await SendMessageAsync(writer);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

reader?.Close();
writer?.Close();




async Task ReceiveMessageAsync(StreamReader reader)
{
    while(true)
    {
        try
        {
            string? message = await reader.ReadLineAsync();
            if (String.IsNullOrEmpty(message)) continue;

            var cursorPosition = Console.GetCursorPosition();
            int left = cursorPosition.Left;
            int top = cursorPosition.Top;

            Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
            Console.SetCursorPosition(0, top);
            Console.Write(message);
            Console.SetCursorPosition(left, top + 1);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            break;
        }
    }
}

async Task SendMessageAsync(StreamWriter writer)
{
    await writer.WriteLineAsync(name);
    await writer.FlushAsync();

    Console.WriteLine("Enter messages and send by pressing key <Enter>");

    while(true)
    {
        string? message = Console.ReadLine();
        await writer.WriteLineAsync(name);
        await writer.FlushAsync();
    }
}