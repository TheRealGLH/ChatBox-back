namespace SocketMessages.Client;

public class ClientMessageDice : ClientMessage
{
    public uint sides;
    public uint amount;
    public int addition;


    public bool Validate()
    {
        return (sides > 0 || amount > 0);
    }
}