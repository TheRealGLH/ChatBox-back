package Messages.Server;

public class ServerMessageText extends ServerMessage{
    private String text;


    public void setText(String text){
        this.text = text;
    }

    public String getText(){
        return this.text;
    }
    
}
