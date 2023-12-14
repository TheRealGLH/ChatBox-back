using ChatBoxSharedObjects.Settings;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ProfileService.Models;

class ProfileDatabaseConnectorMongo : IProfileDatabaseConnector
{
    private static IMongoCollection<Profile> _profileCollection;
    private readonly string _mongoConnectionString;
    private readonly string _mongoDBName;
    private readonly string _mongoCollectionName;
    public ProfileDatabaseConnectorMongo(ChatBoxSharedObjects.Settings.MongoDatabaseSettings databaseSettings)
    {
        this._mongoConnectionString = databaseSettings.ConnectionString;
        this._mongoCollectionName = databaseSettings.CollectionName;
        this._mongoDBName = databaseSettings.DatabaseName;
    }
    public Profile Create(Profile profile)
    {
        GetCollection();
        _profileCollection.InsertOne(profile);
        return profile;
    }

    public void Delete(string characterHash)
    {
        GetCollection();
        _profileCollection.DeleteOne(profile => profile.Id == characterHash);
    }

    public Profile Read(string characterHash)
    {
        GetCollection();
        Profile query = _profileCollection.AsQueryable()
            .Where(profile => profile.Id == characterHash).FirstOrDefault();
        return query;
    }

    public Profile Update(string characterHash, Profile profileToUpdate)
    {
        GetCollection();
        profileToUpdate.Id = characterHash;
        _profileCollection.ReplaceOne(profile => profile.Id == characterHash, profileToUpdate);
        return profileToUpdate;
    }

    void GetCollection()
    {
        // This allows automapping of the camelCase database fields to our models.
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB
        MongoClient mongoClient = new MongoClient(_mongoConnectionString);
        IMongoDatabase database = mongoClient.GetDatabase(_mongoDBName);
        _profileCollection = database.GetCollection<Profile>(_mongoCollectionName);
    }
}