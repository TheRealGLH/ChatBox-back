package Messages.Client;

public class ClientMessage {
    MessageType messageType;

    public void setMessageType(MessageType messageType){
        this.messageType = messageType;
    }

    public MessageType getMessageType(){
        return this.messageType;
    }
    
}
