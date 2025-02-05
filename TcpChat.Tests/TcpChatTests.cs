using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpChatServerApp;
using Xunit;

namespace TcpChat.Tests
{
    
    public class TcpChatTests
    {
        [Fact]
        public void TcpChat_ClientAccepted_()
        {
            // Arrange
            TcpChatServer server = new TcpChatServer();
            TcpClient client = new TcpClient();

            //Act
            server.SubscribeAsync();
            client.Connect(IPAddress.Loopback, 5000);

            //Assert
            Assert.True(client.Client.Connected);
        }
        
        


    }
}
