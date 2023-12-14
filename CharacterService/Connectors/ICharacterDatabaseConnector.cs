namespace CharacterService.Connectors;

using CharacterService.Models;

public interface ICharacterDatabaseConnector
{
    public Character Add(Character character);

    public void Delete(String characterHash);

    public Character Get(String characterHash);

    public Character Update(String characterHash, Character character);

    public List<Character> GetAllUserCharacters(String uuid);

}