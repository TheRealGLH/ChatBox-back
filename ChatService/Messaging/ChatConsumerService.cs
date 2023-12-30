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
    public ChatConsumerService(IRabbitMqService rabbitMqService, IOptions<RabbitMqSettings> options, IServerMessager chatServer) : base(rabbitMqService, options)
    {
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
                    _chatServer.ReceiveText(messageText.content,messageText.speaker);
                        break;
                    case ServerMessageType.DiceResult:
                    ServerMessageDice messageDice = JsonSerializer.Deserialize<ServerMessageDice>(text);
                    _chatServer.ReceiveDice(messageDice.amount,messageDice.sides,messageDice.addition,messageDice.result,messageDice.charName);
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