package Database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import Models.ChatChannel;
import Models.ChatMessage;

public class DatabaseConnectorMySql implements IDatabaseConnector {

    // TODO move this to environment values once we don't have just a local name
    private static final String DB_URL = "jdbc:mysql://localhost:33060/chatbox_messages";
    private static final String DB_NAME = "root";
    private static final String DB_PWD = "root";

    @Override
    public ChatMessage getChatMessage(String messageId) {
        throw new UnsupportedOperationException("Unimplemented method 'getChatMessage'");
    }

    @Override
    public void submitChatMessage(ChatMessage chatMessage) {
        throw new UnsupportedOperationException("Unimplemented method 'submitChatMessage'");

    }

    @Override
    public ChatChannel getChatChannel(String channelId) {
        ChatChannel chatChannel = null;
        try {
            Class.forName("com.mysql.jdbc.Driver");
            Connection con = DriverManager.getConnection(
                    DB_URL, DB_NAME, DB_PWD);
            PreparedStatement statement =con.prepareStatement("SELECT * from channel WHERE id = ?");
            statement.setString(1, channelId);
            ResultSet rs = statement.executeQuery("select * from channel");

            while (rs.next()) {
                System.out.println(rs.getString(1) + "  " + rs.getString(2) + "  " + rs.getString(3));
                chatChannel = new ChatChannel(rs.getString(1), rs.getString(2), rs.getString(3));
            }
            con.close();
        } catch (Exception e) {
            // TODO define the exceptions this should throw in the interface.
            System.out.println(e);
            e.printStackTrace();
        }
        return chatChannel;
    }

    @Override
    public void submitChatChannel(ChatChannel channel) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'submitChatChannel'");
    }

    @Override
    public List<ChatChannel> getAllChatChannels() {
        ArrayList<ChatChannel> channels = new ArrayList<>();
        try {
            Class.forName("com.mysql.jdbc.Driver");
            Connection con = DriverManager.getConnection(
                    DB_URL, DB_NAME, DB_PWD);
            Statement stmt = con.createStatement();
            ResultSet rs = stmt.executeQuery("select * from channel");

            while (rs.next()) {
                System.out.println(rs.getString(1) + "  " + rs.getString(2) + "  " + rs.getString(3));
                channels.add(new ChatChannel(rs.getString(1), rs.getString(2), rs.getString(3)));
            }
            con.close();
        } catch (Exception e) {
            // TODO define the exceptions this should throw in the interface.
            System.out.println(e);
            e.printStackTrace();
        }
        return channels;
    }

}
