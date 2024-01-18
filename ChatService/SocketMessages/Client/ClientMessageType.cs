using System.Text.Json.Serialization;

namespace SocketMessages.Client;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ClientMessageType
{
    Ping,
    SignIn,
    Text,
    Dice
}