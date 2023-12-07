namespace CharacterService.Views;

using CharacterService.Connectors;
using CharacterService.Models;
using Microsoft.Extensions.Options;

public class CharacterStoreDatabase : ICharacterStore
{
    ICharacterDatabaseConnector characterConnector;

    public CharacterStoreDatabase(IOptions<CharacterDatabaseSettings> characterDatabaseSettings){
        characterConnector = new CharacterDatabaseConnectorMongo(characterDatabaseSettings);
    }
    public Character CreateCharacter(Character character)
    {
        Character charAdded = characterConnector.Add(character);
        return charAdded;
    }

    public void DeleteCharacter(string charID)
    {
        GetCharacter(charID);
        characterConnector.Delete(charID);
    }

    public Character GetCharacter(string charID)
    {
        Character character = characterConnector.Get(charID);
        if (character != null) return character;
        throw new KeyNotFoundException("The character with ID " + charID +" does not exist.");
    }

    public Character UpdateCharacter(Character updatedChar, string charID)
    {
        Character oldCharacter = GetCharacter(charID);
        if(oldCharacter != updatedChar)
        {
            characterConnector.Update(charID,updatedChar);
        }
        return updatedChar;
    }
}