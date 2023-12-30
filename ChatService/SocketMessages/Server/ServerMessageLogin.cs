namespace SocketMessages.Server;

class ServerMessageLogin : ServerMessage
{
    public bool success { get; set; }
    public ServerMessageLogin(bool success)
    {
        this.MessageType = ServerMessageType.SignedIn;
        this.success = success;
    }
}