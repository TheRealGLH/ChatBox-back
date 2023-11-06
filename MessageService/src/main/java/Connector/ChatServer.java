package Connector;

import Utilities.ChatLogger;
import Utilities.PropertiesLoader;
import Interfaces.ClientMessager;
import Interfaces.ServerMessager;
import com.google.gson.Gson;

import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.logging.Level;

/**
 * The Game container, will be controlled by an instance of IPlatformGameClient (later via websockets and
 * send it to an instance of this Interface (which will later contain websockets)
 */
public class ChatServer implements ServerMessager {
    List<ClientMessager> joinedClients;//clients which are logged in
    Gson gson = new Gson();
    int minAmountOfPlayers = Integer.parseInt(PropertiesLoader.getPropValues("gameServer.minPlayers","application.properties"));
    Timer timer = new Timer();
    String[] maps;
    String currentMap;

    boolean gameStarted = false;

    public ChatServer() {
        joinedClients = new ArrayList<>();
    }

    @Override
    public void SendMessage(ClientMessager client, String text) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'SendMessage'");
    }

    @Override
    public void SendPing(ClientMessager client) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'SendPing'");
    }

    @Override
    public void JoinClient(ClientMessager client) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'JoinClient'");
    }
}
