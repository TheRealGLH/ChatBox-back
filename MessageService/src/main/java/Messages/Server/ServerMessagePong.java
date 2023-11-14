package Messages.Server;

public class ServerMessagePong extends ServerMessage {
    public ServerMessagePong(){
        this.messageType = MessageType.Pong;
    }
    
}
