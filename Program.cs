using System;
using System.IO;
using System.Text.Json;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mentions
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://github.com/mongodb/mongo-csharp-driver
            // https://github.com/Marfusios/websocket-client

            //TODO
            // mongo bindings
            // figure out the actual mentions
            // fill out commands


            string address = Environment.GetEnvironmentVariable("address");

            Chat chat = new Chat(address);
            chat.ConnectedEvent += new Chat.EventHandlers().ConnectedAsync;
            chat.DisconnectedEvent += new Chat.EventHandlers().DisconnectedAsync;

            chat.MessageEvent += new Chat.Commands().PingAsync;
            chat.Start(3000000);
        }

        
    }
}