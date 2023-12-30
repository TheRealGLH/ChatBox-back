using System.Text.Json.Serialization;

namespace SocketMessages.Server;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServerMessageType
{
    Pong,
    SignedIn,
    Text,
    DiceResult
}