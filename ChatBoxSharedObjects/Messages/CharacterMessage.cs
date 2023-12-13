using System.Text.Json.Serialization;

namespace ChatBoxSharedObjects.Messages;

public class CharacterMessage
{
    public string CharId { get; set; }
    public string OwnerId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CharacterMessageType MessageType { get; set; }

    public CharacterMessage(String CharId, String OwnerId, CharacterMessageType messageType){
        this.CharId = CharId;
        this.OwnerId = OwnerId;
        MessageType = messageType;
    }
}
