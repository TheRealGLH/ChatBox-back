using ChatBoxSharedObjects.Settings;
using Microsoft.Extensions.Options;
using ProfileService.Models;

namespace ProfileService.Views;

public class ProfileStoreDatabase : IProfileStore
{
    IProfileDatabaseConnector _databaseConnector;

    public ProfileStoreDatabase(IOptions<MongoDatabaseSettings> characterDatabaseSettings)
    {
        this._databaseConnector = new ProfileDatabaseConnectorMongo(characterDatabaseSettings.Value);
    }
    public Profile AddProfile(Profile profile)
    {
        return _databaseConnector.Create(profile);
    }

    public void DeleteProfile(string profileId)
    {
        //We get our profile here to double check if we're not deleting something that doesn't exist
        GetProfile(profileId);
        _databaseConnector.Delete(profileId);
    }

    public Profile GetProfile(string profileId)
    {
        Profile get = _databaseConnector.Read(profileId);
        if (get != null) return get;
        throw new KeyNotFoundException("The profile with ID: " + profileId + " does not exist");
    }

    public Profile UpdateProfile(Profile updatedProfile, string profileId)
    {
        Profile oldProfile = GetProfile(profileId);
        if(updatedProfile != oldProfile) updatedProfile = _databaseConnector.Update(profileId, updatedProfile);
        return updatedProfile;
    }
}