using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CharacterService.Models;

public class Character
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public String owner { get; set; }
    [BsonId]
    public ObjectId Id { get; set; }
    public String CharacterName { get; set; }
    public Gender Gender { get; set; }
    public Character(String CharacterName, Gender gender)
    {
        this.CharacterName = CharacterName;
        this.Gender = gender;
    }

    public void Anonymize()
    {
        this.owner = null;
    }
}