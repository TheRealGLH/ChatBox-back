using ProfileService.Models;

public interface IProfileDatabaseConnector
{
    public Profile Create(Profile profile);
    public Profile Read(string characterHash);
    public Profile Update(string characterHash, Profile profileToUpdate);
    public void Delete(string characterHash);
}