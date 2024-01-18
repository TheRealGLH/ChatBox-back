namespace SocketMessages.Client;

public class ClientMessageDice : ClientMessage
{
    public uint Sides {get; set; }
    public uint Amount {get; set; }
    public int Addition {get; set; }


    public bool Validate()
    {
        return (Sides > 0 || Amount > 0);
    }
}