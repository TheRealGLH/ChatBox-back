namespace ChatService.Interfaces;

interface IServerMessager
{
    public void SendPing(IClientMessager client);

    public void SendText(IClientMessager client, string content);

    public void SignIn(IClientMessager client, string characterId, string userId);

    public void RollDice(IClientMessager client, uint count, uint sides, uint addition);
}