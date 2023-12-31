namespace SocketMessages.Client;

public class ClientMessageSignIn : ClientMessage
{
    public string CharacterId { get; set; }

    public bool Validate()
    {
        return (CharacterId != null || CharacterId.Length > 2);
    }
}