package Database;

import Models.ChatChannel;
import Models.ChatMessage;

public class DatabaseConnectorMySql implements IDatabaseConnector {

    

    @Override
    public ChatMessage getChatMessage(String messageId) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'getChatMessage'");
    }

    @Override
    public void submitChatMessage(ChatMessage chatMessage) {
        // TODO Auto-generated method stub

    }

    @Override
    public ChatChannel getChatChannel(String channelId) {
        // TODO Auto-generated method stub
        return null;
    }

    @Override
    public void submitChatChannel(ChatChannel channel) {
        // TODO Auto-generated method stub
        throw new UnsupportedOperationException("Unimplemented method 'submitChatChannel'");
    }

}
