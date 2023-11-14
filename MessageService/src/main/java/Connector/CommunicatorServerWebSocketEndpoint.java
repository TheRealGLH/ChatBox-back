package Connector;

import Interfaces.ClientMessager;
import Interfaces.ServerMessager;
import Messages.Client.ClientMessage;
import Messages.Client.ClientMessageText;
import Messages.Server.ServerMessageText;
import Utilities.ChatLogger;
import com.google.gson.Gson;

import javax.websocket.*;
import javax.websocket.server.ServerEndpoint;
import java.util.HashMap;
import java.util.Map;
import java.util.logging.Level;


@ServerEndpoint("/chat")
public class CommunicatorServerWebSocketEndpoint{

    // All sessions
    private static Map<Session, ClientMessager> sessionClientMessagerMap = new HashMap<>();
    // Map each property to list of sessions that are subscribed to that property
    private static ServerMessager gameServer = new ChatServer();

    @OnOpen
    public void onConnect(Session session) {
        ChatLogger.Log(Level.INFO, "[WebSocket Connected] SessionID: " + session.getId() + " from " + session.getUserProperties().get("javax.websocket.endpoint.remoteAddress"));
        ClientMessager responseClient = new ChatServerMessageSender(session);
        responseClient.setAddress(session.getUserProperties().get("javax.websocket.endpoint.remoteAddress"));
        sessionClientMessagerMap.put(session, responseClient);
        //responseClient.setPlayerNr(sessionIPlatformGameClientMap.size());
        ChatLogger.Log(Level.INFO, "[#sessions]: " + sessionClientMessagerMap.size());
    }

    @OnMessage
    public void onText(String message, Session session) {
        ChatLogger.Log(Level.FINER, "[WebSocket Session ID] : " + session.getId() + " sent socket message");
        handleMessageFromClient(message, session);
    }

    @OnClose
    public void onClose(CloseReason reason, Session session) {
        ChatLogger.Log(Level.INFO, "[WebSocket Session ID] : " + session.getId() + " [Socket Closed]: " + reason + " from " + session.getUserProperties().get("javax.websocket.endpoint.remoteAddress"));
        //gameServer.removePlayer(sessionIPlatformGameClientMap.get(session));
        sessionClientMessagerMap.remove(session);
    }

    @OnError
    public void onError(Throwable cause, Session session) {
        ChatLogger.Log(Level.SEVERE, "[WebSocket Session ID] : " + session.getId() + " was forcefully disconnected because: " + cause.getMessage());
        //gameServer.removePlayer(sessionIPlatformGameClientMap.get(session));
        sessionClientMessagerMap.remove(session);
    }

    // Handle incoming message from client
    private void handleMessageFromClient(String jsonMessage, Session session) {
        Gson gson = new Gson();
        Messages.Client.MessageType messageType = gson.fromJson(jsonMessage, ClientMessage.class).getMessageType();
        ClientMessager client = sessionClientMessagerMap.get(session);
        if (messageType != null) {
            switch (messageType) {
                case Ping:
                gameServer.SendPing(client);
                    break;
                case Text:
                ServerMessageText messageText = gson.fromJson(jsonMessage, ServerMessageText.class);
                gameServer.SendMessage(client, messageText.getText());
                    break;
            }
        } else {
            ChatLogger.Log(Level.SEVERE, "Malformed message received from " + client.getAddress(), jsonMessage);
        }
    }
}