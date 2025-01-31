using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChatServerApp
{
    public class TcpChatClient
    {
        // Second branch
        public Guid Id { get; }
        public StreamReader Reader { get; }
        public StreamWriter Writer { get; }

        TcpClient tcpClient;
        TcpChatServer server;

        public TcpChatClient(TcpClient tcpClient, TcpChatServer server)
        {
            this.tcpClient = tcpClient;
            this.server = server;

            NetworkStream stream = tcpClient.GetStream();
            Reader = new StreamReader(stream);
            Writer = new StreamWriter(stream);
        }

        public async Task ProcessAsync()
        {
            try
            {
                string? clientName = await Reader.ReadLineAsync();
                string? message = $"{clientName} logged into the chat";

                await server.SendMessageAsync(Id, message);
                Console.WriteLine(message);

                while(true)
                {
                    try
                    {
                        message = await Reader.ReadLineAsync();
                        
                        if (message == null) continue;

                        message = $"{clientName}: {message}";
                        await server.SendMessageAsync(Id, message);
                        Console.WriteLine(message);
                    }
                    catch(Exception ex)
                    {
                        message = $"{clientName} left the chat";
                        await server.SendMessageAsync(Id, message);
                        Console.WriteLine(message);
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.RemoveClient(Id);
            }
        }

        public void Close()
        {
            Reader.Close();
            Writer.Close();
            tcpClient.Close();
        }
    }
}
