package Messages.Client;

public class ClientMessagePong extends ClientMessage {

    public ClientMessagePong(){
        super.messageType = MessageType.Ping;
    }
    
}
