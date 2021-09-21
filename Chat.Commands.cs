using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace mentions
{
    partial class Chat
    {
        public class Commands
        {
            public async Task PingAsync(Message msg, WebsocketClient client)
            {
                if (msg.data == "ping") client.Send(Message.FormatMessage("Pong"));
            }
        }
    }
}
