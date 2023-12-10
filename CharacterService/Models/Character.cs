using System.Buffers;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace CharacterService.Models;

public class Character
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public String owner { get; set; }
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public String CharacterName { get; set; }
    public String Species { get; set; }
    public String Pronouns { get; set; }
    public String Bio { get; set; }


    public Character(String characterName, String pronouns, String species, String bio)
    {
        this.CharacterName = characterName;
        this.Pronouns = pronouns;
        this.Species = species;
        this.Bio = bio;
    }


    public Character(CharacterSubmission characterSubmission, String owner)
    {
        this.CharacterName = characterSubmission.characterName;
        this.Pronouns = characterSubmission.Pronouns;
        this.Species = characterSubmission.Species;
        this.Bio = characterSubmission.Bio;
        this.owner = owner;
    }

    public void UpdateCharacter(CharacterSubmission characterSubmission)
    {
        this.CharacterName = characterSubmission.characterName;
        this.Pronouns = characterSubmission.Pronouns;
        this.Species = characterSubmission.Species;
        this.Bio = characterSubmission.Bio; 
    }

    public void Anonymize(String requesterId)
    {
        if(string.Equals(requesterId, owner)) return;
        this.owner = null;
    }
}