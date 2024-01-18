namespace ChatService.Interfaces;

public interface IClientMessager
{
    public void ReceivePong();

    public void ReceiveLoginStatus(bool success);

    public void ReceiveText(string content, string characterName);

    public void ReceiveDiceResult(uint sides, uint amount, int addition, int outcome, string characterName);
}