using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace ChatBoxSharedObjects.Models;

public class StoredResource
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public String owner { get; set; }
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public StoredResource(string owner){
        this.owner = owner;
    }
    public void Anonymize(String requesterId)
    {
        if(string.Equals(requesterId, owner)) return;
        this.owner = null;
    }
}