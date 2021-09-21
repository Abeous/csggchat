using System;
using System.Collections.Generic;
using System.Text;
using Websocket.Client;
using System.Threading.Tasks;
using System.Threading;

namespace mentions
{
    // add user list and user join, leave etc
    partial class Chat
    {
        private readonly AsyncEvent<Func<ReconnectionType, Task>> _connectedEvent = new AsyncEvent<Func<ReconnectionType, Task>>();
        public event Func<ReconnectionType, Task> ConnectedEvent
        {
            add { _connectedEvent.Add(value); }
            remove { _connectedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<DisconnectionType, Task>> _disconnectedEvent = new AsyncEvent<Func<DisconnectionType, Task>>();
        public event Func<DisconnectionType, Task> DisconnectedEvent
        {
            add { _disconnectedEvent.Add(value); }
            remove { _disconnectedEvent.Remove(value); }
        }

        private readonly AsyncEvent<Func<Message, WebsocketClient, Task>> _messageEvent = new AsyncEvent<Func<Message, WebsocketClient, Task>>();
        public event Func<Message, WebsocketClient, Task> MessageEvent
        {
            add { _messageEvent.Add(value); }
            remove { _messageEvent.Remove(value); }
        }


        public class EventHandlers
        {
            public async Task ConnectedAsync(ReconnectionType type)
            {
                Console.WriteLine("connected" + type.ToString());
            }

            public async Task DisconnectedAsync(DisconnectionType type)
            {
                Console.WriteLine("disconnected" + type.ToString());
            }

        }


    }
}
