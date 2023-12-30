using ChatService.Interfaces;

namespace ChatService.Connectors;

class ChatServer : IServerMessager
{
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
        throw new NotImplementedException();
    }
}