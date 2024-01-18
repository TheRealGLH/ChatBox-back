namespace SocketMessages.Server;

public class ServerMessageText : ServerMessage
{
    public string MessageContent { get; set; }
    public string CharacterName { get; set; }
    public ServerMessageText(string MessageContent, string CharacterName)
    {
        this.MessageType = ServerMessageType.Text;
        this.MessageContent = MessageContent;
        this.CharacterName = CharacterName;
    }
}