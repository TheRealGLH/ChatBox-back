package Connector;

import Utilities.ChatLogger;
import com.google.gson.Gson;
import Interfaces.ClientMessager;
import Messages.Server.ServerMessagePing;
import Messages.Server.ServerMessageText;

import javax.websocket.Session;
import java.util.logging.Level;

public class ChatServerMessageSender implements ClientMessager {

    private Session session;
    private Gson gson = new Gson();
    Object address;

    public ChatServerMessageSender(Session session) {
        this.session = session;
    }

    
/* 
    private void sendMessage(PlatformGameResponseMessage responseMessage) {
        String json = gson.toJson(responseMessage);
        PlatformLogger.Log(Level.FINEST, "Sending client: " + session.getId() + " "
                + session.getUserProperties().get("javax.websocket.endpoint.remoteAddress")
                + responseMessage);
        session.getAsyncRemote().sendText(json);
    }
*/
    @Override
    public void SendTextMessage(ServerMessageText responseMessage) {
                String json = gson.toJson(responseMessage);
        ChatLogger.Log(Level.FINEST, "Sending client: " + session.getId() + " "
                + session.getUserProperties().get("javax.websocket.endpoint.remoteAddress")
                + responseMessage);
        session.getAsyncRemote().sendText(json);
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'SendTextMessage'");
    }

    @Override
    public void SendPong() {
        // TODO Auto-generated method stub
        ServerMessagePing responseMessage = new ServerMessagePing();
        String json = gson.toJson(responseMessage);
        ChatLogger.Log(Level.FINEST, "Sending client: " + session.getId() + " "
                + session.getUserProperties().get("javax.websocket.endpoint.remoteAddress")
                + responseMessage);
        session.getAsyncRemote().sendText(json);
    }

    @Override
    public Object getAddress() {
        return this.address;
    }

    @Override
    public void setAddress(Object o) {
        this.address = o;
    }
}
