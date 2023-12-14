using ProfileService.Models;

namespace ProfileService.Views;

public interface IProfileStore
{
    public Profile AddProfile(Profile profile);
    public Profile GetProfile(string profileId);
    public Profile UpdateProfile(Profile profile, string profileId);
    public void DeleteProfile(string profileId);

}