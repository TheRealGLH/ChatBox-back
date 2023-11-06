package Interfaces;

public interface ServerMessager {

    public void JoinClient(ClientMessager client);
    
    public void SendMessage(ClientMessager client, String text);

    public void SendPing(ClientMessager client);
}
