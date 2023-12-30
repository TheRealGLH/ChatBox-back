namespace SocketMessages.Server;

public class ServerMessageDice : ServerMessage
{
    public uint sides { get; set; }
    public uint amount { get; set; }
    public int addition { get; set; }
    public int result { get; set; }
    public string charName { get; set; }

    public ServerMessageDice(uint sides, uint amount, int addition, int result, string charName)
    {
        this.MessageType = ServerMessageType.DiceResult;
        this.sides = sides;
        this.amount = amount;
        this.addition = addition;
        this.result = result;
        this.charName = charName;
    }
}