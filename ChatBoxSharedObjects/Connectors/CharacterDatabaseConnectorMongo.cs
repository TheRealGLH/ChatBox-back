using ChatBoxSharedObjects.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ChatBoxSharedObjects.Connectors;

public class CharacterDatabaseConnectorMongo : ICharacterDatabaseConnector
{
    private static IMongoCollection<Character> _characterCollection;
    //TODO: Make this an environment value
    private string MongoConnectionString;
    private string mongoDBName;
    private string mongoCharCollectionName;
    public CharacterDatabaseConnectorMongo(IOptions<ChatBoxSharedObjects.Settings.MongoDatabaseSettings> characterDatabaseSettings)
    {
        MongoConnectionString = characterDatabaseSettings.Value.ConnectionString;
        mongoDBName = characterDatabaseSettings.Value.DatabaseName;
        mongoCharCollectionName = characterDatabaseSettings.Value.CollectionName;
    }
    public Character Add(Character character)
    {
        GetCollection();
        _characterCollection.InsertOne(character);
        return character;
    }

    public void Delete(string characterHash)
    {
        GetCollection();
        _characterCollection.DeleteOne(character => character.Id == characterHash);
    }

    public Character Get(string characterHash)
    {
        GetCollection();
        // start-find-linq
        Character query = _characterCollection.AsQueryable()
            .Where(character => character.Id == characterHash).FirstOrDefault();
        //     .Where(character => character.Id == ObjectId.Parse(characterHash)).FirstOrDefault();
        // end-find-linq
        return query;
    }

    public List<Character> GetAllUserCharacters(String uuid)
    {
        GetCollection();
        return _characterCollection.AsQueryable()
    .Where(r => r.owner == uuid).ToList();
    }

    public Character Update(string characterHash, Character character)
    {
        GetCollection();
        character.Id = characterHash;
        _characterCollection.ReplaceOne(character => character.Id == characterHash, character);
        return character;
    }

    void GetCollection()
    {
        // This allows automapping of the camelCase database fields to our models.
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB
        MongoClient mongoClient = new MongoClient(MongoConnectionString);
        IMongoDatabase database = mongoClient.GetDatabase(mongoDBName);
        _characterCollection = database.GetCollection<Character>(mongoCharCollectionName);
    }
}