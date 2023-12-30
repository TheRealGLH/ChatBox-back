namespace ChatService.Interfaces;

public interface IServerMessager
{
    public void SendPing(IClientMessager client);

    public void SendText(IClientMessager client, string content);

    public void ReceiveText(string content, string charName);

    public void SignIn(IClientMessager client, string characterId, string userId);

    public void SignOut(IClientMessager client);

    public void RollDice(IClientMessager client, uint count, uint sides, int addition);

    public void ReceiveDice(uint count, uint sides, int addition, int outcome, string charName);


}