using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.WebSockets;
using Websocket.Client;
using System.Threading;
using System.Threading.Tasks;

namespace mentions
{
    partial class Chat
    {
        public ManualResetEvent exitEvent = new ManualResetEvent(false);
        public Uri url;

        public Chat(string wsAddress)
        {
            url = new Uri(wsAddress);
        }

        public async void Start(int timeout)
        {
            string jwtToken = Environment.GetEnvironmentVariable("jwt");
            CookieContainer container = new();
            container.Add(url, new Cookie("jwt",jwtToken));

            var factory = new Func<ClientWebSocket>(() => new ClientWebSocket { Options = { Cookies = container } });
            using (var client = new WebsocketClient(this.url, factory))
            {

                client.ReconnectTimeout = TimeSpan.FromSeconds(timeout);

                client.ReconnectionHappened.Subscribe(info =>
                     _connectedEvent.InvokeAsync(info.Type));

                client.DisconnectionHappened.Subscribe(info =>
                    _disconnectedEvent.InvokeAsync(info.Type));

                client.MessageReceived.Subscribe(msg => 
                    _messageEvent.InvokeAsync(MessageHandler(msg), client));

                client.Start();

                this.exitEvent.WaitOne();
            }
        }
    }
}
