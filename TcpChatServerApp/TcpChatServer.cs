using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChatServerApp
{
    public class TcpChatServer
    {
        IPAddress ip; 
        int port;

        TcpListener listener;
        List<TcpChatClient> clients;

        public TcpChatServer()
        {
            //ip = Dns.GetHostAddresses(Dns.GetHostName(), AddressFamily.InterNetwork)[0];
            ip = IPAddress.Loopback;
            port = 5000;
            listener = new(ip, port);

            clients = new List<TcpChatClient>();
        }

        public async Task SubscribeAsync()
        {
            try
            {
                listener.Start();
                Console.WriteLine($"Server {ip.ToString()} starting");

                while(true)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    TcpChatClient client = new TcpChatClient(tcpClient, this);

                    clients.Add(client);
                    Task.Run(client.ProcessAsync);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                foreach(TcpChatClient client in clients)
                    client.Close();
                listener.Stop();
            }
        }

        public async Task SendMessageAsync(Guid id, string message)
        {
            foreach(TcpChatClient client in clients)
            {
                if(client.Id != id)
                {
                    await client.Writer.WriteLineAsync(message);
                    await client.Writer.FlushAsync();
                }
            }
        }

        public void RemoveClient(Guid id)
        {
            TcpChatClient? client = clients.FirstOrDefault(c => c.Id == id);
            if(client is not null)
                clients.Remove(client);
            client?.Close();
        }
    }
}
