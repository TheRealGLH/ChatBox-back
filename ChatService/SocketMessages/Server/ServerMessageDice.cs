namespace SocketMessages.Server;

public class ServerMessageDice : ServerMessage
{
    public uint Sides { get; set; }
    public uint Amount { get; set; }
    public int Addition { get; set; }
    public int Result { get; set; }
    public string CharacterName { get; set; }

    public ServerMessageDice(uint Sides, uint Amount, int Addition, int Result, string CharacterName)
    {
        this.MessageType = ServerMessageType.DiceResult;
        this.Sides = Sides;
        this.Amount = Amount;
        this.Addition = Addition;
        this.Result = Result;
        this.CharacterName = CharacterName;
    }
}