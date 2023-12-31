namespace SocketMessages.Client;

public class ClientMessageDice : ClientMessage
{
    public uint Sides;
    public uint Amount;
    public int Addition;


    public bool Validate()
    {
        return (Sides > 0 || Amount > 0);
    }
}