package Interfaces;

import Messages.Client.ClientMessageText;
import Messages.Server.ServerMessageText;

public interface ClientMessager {
    
    public void SendTextMessage(ServerMessageText clientMessageText);

    public void SendPong();


    public Object getAddress();

    void setAddress(Object address);
}
