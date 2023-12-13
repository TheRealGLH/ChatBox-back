using ChatBoxSharedObjects.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProfileService.Models;
public class Profile : StoredResource
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string CharacterHash { get; set; }
    public string ProfileText { get; set; }
    public ushort Age { get; set; }
    public string AgeDescription { get; set; }
    public string Height { get; set; }
    public string Occupation { get; set; }
    public string PersonalityDescription { get; set; }
    public string Location { get; set; }


    public Profile(string characterHash, string owner) : base(owner)
    {
        this.CharacterHash = characterHash;
        this.ProfileText = "";
        this.Age = 0;
        this.AgeDescription = "";
        this.Occupation = "";
        this.PersonalityDescription = "";
        this.Location = "";
    }
}