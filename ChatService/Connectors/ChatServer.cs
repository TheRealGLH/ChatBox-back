using ChatBoxSharedObjects.Connectors;
using ChatBoxSharedObjects.Models;
using ChatService.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ChatService.Connectors;

public class ChatServer : IServerMessager
{
    private readonly ICharacterDatabaseConnector _characterDatabaseConnector;
    Dictionary<IClientMessager, ConnectedCharacter> connectedCharacters = new Dictionary<IClientMessager, ConnectedCharacter>();

    public ChatServer(ICharacterDatabaseConnector characterDatabaseConnector)
    {
        this._characterDatabaseConnector = characterDatabaseConnector;
    }
    public void RollDice(IClientMessager client, uint count, uint sides, uint addition)
    {
        throw new NotImplementedException();
    }

    public void SendPing(IClientMessager client)
    {
        client.ReceivePong();
    }

    public void SendText(IClientMessager client, string content)
    {
        throw new NotImplementedException();
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
            connectedCharacters.Add(client,new ConnectedCharacter(character.Id,character.owner));
            client.ReceiveLoginStatus(true);
        }
    }

    public void SignOut(IClientMessager client)
    {
        throw new NotImplementedException();
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