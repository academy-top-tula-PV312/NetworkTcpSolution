using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientOnSocketApp
{
    internal static class Examples
    {
        public static async Task TcpClientSocketWelcomeExample()
        {
            int port = 80;
            string url = "www.yandex.ru";

            using Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                await clientSocket.ConnectAsync(url, port);
                Console.WriteLine("Connected!");
                Console.WriteLine($"End point local: {clientSocket.LocalEndPoint}");
                Console.WriteLine($"End point remote: {clientSocket.RemoteEndPoint}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await clientSocket.DisconnectAsync(true);
            }

        }
    }
}
