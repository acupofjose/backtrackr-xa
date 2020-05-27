using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BacktrackrXA.Extensions;
using BacktrackrXA.Injections;
using Newtonsoft.Json;
using Websocket.Client;
using Websocket.Client.Models;

namespace BacktrackrXA.Framework
{
    public class SocketClient
    {
        private static SocketClient _instance;
        public static SocketClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SocketClient();
                return _instance;
            }
        }

        private WebsocketClient client;
        private Uri url;

        public bool IsStarted { get => client != null ? client.IsStarted : false; }
        public bool IsRunning { get => client != null ? client.IsRunning : false; }

        private SocketClient() { }

        public Task<bool> Connect()
        {
            var tsc = new TaskCompletionSource<bool>();

            Task.Run(async () =>
            {
                try
                {
                    if (client != null)
                    {
                        if (client.IsRunning || client.IsStarted)
                            await client.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, null);

                        client.Dispose();
                        client = null;
                    }

                    url = new Uri($"ws://{Preferences.ServerHost}:{Preferences.ServerPort}");

                    client = new WebsocketClient(url);
                    client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                    client.ReconnectionHappened.Subscribe(Client_ReconnectionHappened);
                    client.DisconnectionHappened.Subscribe(Client_DisconnectionHappened);
                    client.MessageReceived.Subscribe(Client_MessageReceived);

                    await client.Start();
                    tsc.SetResult(client.IsRunning);
                }
                catch (Exception ex)
                {
                    tsc.SetException(ex);
                }
            });

            return tsc.Task;
        }

        public void PostLocation(Location location)
        {
            var json = JsonConvert.SerializeObject(location);
            client.Send(json);
        }

        private void Client_ReconnectionHappened(ReconnectionInfo info)
        {
            Debug.WriteLine($"Reconnection happened, type: {info.Type}");
        }

        private void Client_DisconnectionHappened(DisconnectionInfo info)
        {
            Debug.WriteLine($"Disconnection happened, type: {info.Type}");
        }

        private void Client_MessageReceived(ResponseMessage message)
        {
            if (message.Text.Contains("ping"))
                client.Send("pong");
            else
                Debug.WriteLine($"Message received: {message}");
        }
    }
}
