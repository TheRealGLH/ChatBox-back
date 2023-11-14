package Messages.Client;

public class ClientMessagePing extends ClientMessage {

    public ClientMessagePing(){
        super.messageType = MessageType.Ping;
    }
    
}
