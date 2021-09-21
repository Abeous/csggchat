using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Websocket.Client;

namespace mentions
{
    partial class Chat
    {

        static Message MessageHandler(ResponseMessage msg)
        {
            return new Message(msg);
        }

        public class Message
        {
            public Message (ResponseMessage msg)
            {
                string[] splitMsg = msg.Text.Split(" ", 2);
                rawJson = splitMsg[1];
                type = splitMsg[0];
                if (type != "MSG") return;

                _message message = _message.Deserialize(rawJson);
                user = User.Deserialize(rawJson);

                features = message.features;
                timestamp = message.timestamp;
                data = message.data;
            }
            
            public User user { get; set; }
            public string[] features { get; set; }
            public long timestamp { get; set; }
            public string data { get; set; }
            public string type { get; set; }
            public string rawJson { get; set; }

            public static string FormatMessage(string msg)
            {
                return $"MSG {{\"data\":\"{msg}\"}}";
            }
        }

        internal class _message
        {
            public string nick { get; set; }
            public string[] features { get; set; }
            public long timestamp { get; set; }
            public string data { get; set; }
            //public string[] entities { get; set; }

            public static _message Deserialize(string json) => JsonSerializer.Deserialize<_message>(json, null);
        }

        public class User
        {
            public string nick { get; set; }
            public string[] features { get; set; }

            public static User Deserialize(string json) => JsonSerializer.Deserialize<User>(json, null);
        }
    }
}
