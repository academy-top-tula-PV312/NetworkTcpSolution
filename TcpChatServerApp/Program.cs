// SERVER TCP CHAT

using System.Net;
using System.Net.Sockets;
using TcpChatServerApp;

TcpChatServer server = new();
await server.SubscribeAsync();