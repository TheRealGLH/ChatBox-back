namespace SocketMessages.Client;

public class ClientMessageSignIn : ClientMessage
{
    public string characterId { get; set; }

    public bool Validate()
    {
        return (characterId != null || characterId.Length > 2);
    }
}