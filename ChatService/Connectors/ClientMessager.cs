using System.Data.SqlTypes;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using ChatService.Interfaces;
using SocketMessages.Server;

namespace ChatService.Connectors
{
    public class ClientMessager : IClientMessager
    {
        private WebSocket _webSocketClient;
        public ClientMessager(WebSocket webSocketClient)
        {
            this._webSocketClient = webSocketClient;
        }

        public void ReceiveDiceResult(uint sides, uint amount, int addition, int outcome)
        {
            throw new NotImplementedException();
        }

        public void ReceiveLoginStatus(bool success)
        {
            ServerMessageLogin serverMessage = new ServerMessageLogin(success);
            string json = JsonSerializer.Serialize(serverMessage);
            SendText(json);
        }

        public void ReceivePong()
        {
            ServerMessagePong pong = new ServerMessagePong();
            string json = JsonSerializer.Serialize(pong);
            SendText(json);
        }

        public void ReceiveText(string content, string characterName)
        {
            throw new NotImplementedException();
        }


        void SendText(string json)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(json);
             _webSocketClient.SendAsync(
                 new ArraySegment<byte>(buffer, 0, buffer.Length),
                 WebSocketMessageType.Text,
                 true,
                 CancellationToken.None);
        }
    }
}