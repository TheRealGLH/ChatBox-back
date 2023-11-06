package Interfaces;

import Messages.Client.ClientMessageText;

public interface ClientMessager {
    
    public void SendTextMessage(ClientMessageText clientMessageText);

    public void SendPong();


    public Object getAddress();

    void setAddress(Object address);
}
