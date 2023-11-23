namespace CharacterService.Views;

using CharacterService.Connectors;
using CharacterService.Models;
public class CharacterStoreDatabase : ICharacterStore
{
    ICharacterDatabaseConnector characterStore;

    public CharacterStoreDatabase(){
        
    }
    public String CreateCharacter(Character character)
    {
        String characterHash = characterStore.Add(character);
        return characterHash;
    }

    public void DeleteCharacter(string charID)
    {
        GetCharacter(charID);
        characterStore.Delete(charID);
    }

    public Character GetCharacter(string charID)
    {
        Character character = characterStore.Get(charID);
        if (character != null) return character;
        throw new KeyNotFoundException("The character with ID " + charID +" does not exist.");
    }

    public Character UpdateCharacter(Character updatedChar, string charID)
    {
        Character oldCharacter = GetCharacter(charID);
        if(oldCharacter != updatedChar)
        {
            characterStore.Update(charID,updatedChar);
        }
        return updatedChar;
    }
}