namespace SocketMessages.Server;

public class ServerMessageText : ServerMessage
{
    public string content { get; set; }
    public string speaker { get; set; }
    public ServerMessageText(string content, string speaker)
    {
        this.MessageType = ServerMessageType.Text;
        this.content = content;
        this.speaker = speaker;
    }
}