using System.Text.Json;
using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Settings;
using ChatService.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using SocketMessages.Client;
using SocketMessages.Server;

public class ChatConsumerService : RabbitMqConsumerService
{
    IServerMessager _chatServer;
    const string queueNameChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    static Random random = new Random();
    const int QueueLength = 16;
    public ChatConsumerService(IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options, IServerMessager chatServer) : base(rabbitMqService, options)
    {
        _configuration.QueueName = _configuration.QueueName + new string(Enumerable.Repeat(queueNameChars, QueueLength)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        _chatServer = chatServer;
    }
    public override void OnMessageReceived(BasicDeliverEventArgs eventArgs)
    {
        ServerMessage IncomingMessage;
        var body = eventArgs.Body.ToArray();
        var text = System.Text.Encoding.UTF8.GetString(body);
        try
        {
            IncomingMessage = JsonSerializer.Deserialize<ServerMessage>(text);
            if (IncomingMessage != null)
            {
                switch (IncomingMessage.MessageType)
                {
                    case ServerMessageType.Text:
                        ServerMessageText messageText = JsonSerializer.Deserialize<ServerMessageText>(text);
                        _chatServer.ReceiveText(messageText.MessageContent, messageText.CharacterName);
                        break;
                    case ServerMessageType.DiceResult:
                        ServerMessageDice messageDice = JsonSerializer.Deserialize<ServerMessageDice>(text);
                        _chatServer.ReceiveDice(messageDice.Amount, messageDice.Sides, messageDice.Addition, messageDice.Result, messageDice.CharacterName);
                        break;
                    default:
                        //We'll see :^)
                        break;
                }
            }
            else throw new JsonException("Deserialized message " + eventArgs.Body.ToString() + " was comehow null?");
        }
        catch (JsonException e)
        {
            Console.Error.WriteLine(e.Message);
            Console.Error.WriteLine(e.StackTrace);
        }
    }
}