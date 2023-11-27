using CharacterService.Connectors;
using CharacterService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

class CharacterDatabaseConnectorMongo : ICharacterDatabaseConnector
{
    private static IMongoCollection<Character> _characterCollection;
    //TODO: Make this an environment value
    private string MongoConnectionString;
    private string mongoDBName;
    private string mongoCharCollectionName;
    public CharacterDatabaseConnectorMongo(IOptions<CharacterDatabaseSettings> characterDatabaseSettings)
    {
        MongoConnectionString = characterDatabaseSettings.Value.ConnectionString;
        mongoDBName = characterDatabaseSettings.Value.DatabaseName;
        mongoCharCollectionName = characterDatabaseSettings.Value.CharacterCollectionName;
    }
    public string Add(Character character)
    {
        GetCollection();
        _characterCollection.InsertOne(character);
        return character.Id.ToString();
    }

    public void Delete(string characterHash)
    {
        GetCollection();
        _characterCollection.DeleteOne(character => character.Id == ObjectId.Parse(characterHash));
    }

    public Character Get(string characterHash)
    {
        GetCollection();
        // start-find-linq
        Character query = _characterCollection.AsQueryable()
            .Where(character => character.Id == ObjectId.Parse(characterHash)).FirstOrDefault();
        // end-find-linq
        return query;
    }

    public Character Update(string characterHash, Character character)
    {
        GetCollection();
        character.Id = ObjectId.Parse(characterHash);
        _characterCollection.ReplaceOne(character => character.Id == ObjectId.Parse(characterHash),character);
        return character;
    }

    void GetCollection()
    {
        // This allows automapping of the camelCase database fields to our models.
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        MongoClient mongoClient = new MongoClient(MongoConnectionString);
        IMongoDatabase restaurantsDatabase = mongoClient.GetDatabase(mongoDBName);
        _characterCollection = restaurantsDatabase.GetCollection<Character>(mongoCharCollectionName);
    }
}