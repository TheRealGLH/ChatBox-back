package Messages.Client;

public abstract class ClientMessage {
    MessageType messageType;

    public void setMessageType(MessageType messageType){
        this.messageType = messageType;
    }

    public MessageType getMessageType(){
        return this.messageType;
    }
    
}
