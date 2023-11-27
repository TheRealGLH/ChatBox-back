package Database;

import java.util.List;

import Models.ChatChannel;
import Models.ChatMessage;

/**
 * IDatabaseConnector
 */
public interface IDatabaseConnector {

    public ChatMessage getChatMessage(String messageId);

    public void submitChatMessage(ChatMessage chatMessage);

    public ChatChannel getChatChannel(String channelId);

    public List<ChatChannel> getAllChatChannels();

    public void submitChatChannel(ChatChannel channel);

}