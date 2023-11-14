package Messages.Server;

public class ServerMessage {
    MessageType messageType;

    public void setMessageType(MessageType messageType){
        this.messageType = messageType;
    }

    public MessageType getMessageType(){
        return this.messageType;
    }
    
}
