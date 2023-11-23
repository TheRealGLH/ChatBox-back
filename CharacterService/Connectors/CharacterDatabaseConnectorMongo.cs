using CharacterService.Connectors;
using CharacterService.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

class CharacterDatabaseConnectorMongo : ICharacterDatabaseConnector
{
    private static IMongoCollection<Character> _employeeCollection;
    //TODO: Make this an environment value
    private const string MongoConnectionString = "mongodb://localhost:55000";
    public CharacterDatabaseConnectorMongo()
    {

    }
    public string Add(Character character)
    {
        _employeeCollection.InsertOne(character);
        return character.Id.ToString();
    }

    public void Delete(string characterHash)
    {
        throw new NotImplementedException();
    }

    public Character Get(string characterHash)
    {
        GetCollection();
        // start-find-linq
        Character query = _employeeCollection.AsQueryable()
            .Where(e => e.Id == ObjectId.Parse(characterHash)).FirstOrDefault();
        // end-find-linq
        return query;
    }

    public Character Update(string characterHash, Character character)
    {
        throw new NotImplementedException();
    }

    void GetCollection()
    {
        // This allows automapping of the camelCase database fields to our models. 
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        MongoClient mongoClient = new MongoClient(MongoConnectionString);
        IMongoDatabase restaurantsDatabase = mongoClient.GetDatabase("employees");
        _employeeCollection = restaurantsDatabase.GetCollection<Character>("characters");
    }
}