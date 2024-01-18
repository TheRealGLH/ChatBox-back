namespace SocketMessages.Client;

public class ClientMessageText : ClientMessage
{


    public string MessageContent { get; set; }


        public bool Validate()
    {
        return (MessageContent != null || MessageContent.Length > 2);
    }

}