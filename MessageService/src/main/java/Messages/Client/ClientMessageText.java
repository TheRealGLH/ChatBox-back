package Messages.Client;

public class ClientMessageText extends ClientMessage {
    private String text;


    public void setText(String text){
        this.text = text;
    }

    public String getText(){
        return this.text;
    }
    
}
