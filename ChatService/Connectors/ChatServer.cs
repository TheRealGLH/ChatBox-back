using CharacterService.Messaging;
using ChatBoxSharedObjects.Connectors;
using ChatBoxSharedObjects.Models;
using ChatService.Interfaces;
using Microsoft.IdentityModel.Tokens;
using SocketMessages.Server;

namespace ChatService.Connectors;

public class ChatServer : IServerMessager
{
    private readonly ICharacterDatabaseConnector _characterDatabaseConnector;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    private Random random = new Random();
    Dictionary<IClientMessager, ConnectedCharacter> connectedCharacters = new Dictionary<IClientMessager, ConnectedCharacter>();

    public ChatServer(ICharacterDatabaseConnector characterDatabaseConnector, IRabbitMqProducer rabbitMqProducer)
    {
        this._characterDatabaseConnector = characterDatabaseConnector;
        this._rabbitMqProducer = rabbitMqProducer;
    }

    public void ReceiveDice(uint count, uint sides, int addition, int outcome, string charName)
    {
        foreach (KeyValuePair<IClientMessager, ConnectedCharacter> entry in connectedCharacters)
        {
            entry.Key.ReceiveDiceResult(sides, count, addition, outcome, charName);
        }
    }

    public void ReceiveText(string content, string charName)
    {
        foreach (KeyValuePair<IClientMessager, ConnectedCharacter> entry in connectedCharacters)
        {
            entry.Key.ReceiveText(content, charName);
        }
    }

    public void RollDice(IClientMessager client, uint count, uint sides, int addition)
    {
        if (connectedCharacters.ContainsKey(client))
        {
            int outcome = 0;
            for (int i = 0; i < count; i++)
            {
                outcome += random.Next(1, (int)sides);
            }
            outcome += addition;
            ServerMessageDice serverMessage = new ServerMessageDice(sides, count, addition, outcome, connectedCharacters[client].charName);
            this._rabbitMqProducer.SendCreationMessage(serverMessage);
        }
    }

    public void SendPing(IClientMessager client)
    {
        client.ReceivePong();
    }

    public void SendText(IClientMessager client, string content)
    {
        if (connectedCharacters.ContainsKey(client))
        {
            ServerMessageText messageText = new ServerMessageText(content, connectedCharacters[client].charName);
            _rabbitMqProducer.SendCreationMessage(messageText);
        }
    }

    public void SignIn(IClientMessager client, string characterId, string userId)
    {
        Character character = _characterDatabaseConnector.Get(characterId);
        if (character == null || character.owner != userId)
        {
            client.ReceiveLoginStatus(false);
        }
        else
        {
            connectedCharacters.Add(client, new ConnectedCharacter(character.Id, character.CharacterName));
            client.ReceiveLoginStatus(true);
        }
    }

    public void SignOut(IClientMessager client)
    {
        connectedCharacters.Remove(client);
    }
}

class ConnectedCharacter
{
    public string charUUID { get; set; }
    public string charName { get; set; }

    public ConnectedCharacter(string charUUID, string charName)
    {
        this.charUUID = charUUID;
        this.charName = charName;
    }
}