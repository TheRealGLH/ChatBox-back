namespace SocketMessages.Server;

public class ServerMessagePong : ServerMessage
{
    public ServerMessagePong()
    {
        this.MessageType = ServerMessageType.Pong;
    }
}