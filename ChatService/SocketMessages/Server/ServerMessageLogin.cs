namespace SocketMessages.Server;

class ServerMessageLogin : ServerMessage
{
    public bool Success { get; set; }
    public ServerMessageLogin(bool Success)
    {
        this.MessageType = ServerMessageType.SignedIn;
        this.Success = Success;
    }
}