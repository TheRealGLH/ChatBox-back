using CharacterService.Controllers;

class CharacterStoreMock : ICharacterStore
{
    Dictionary<String, Character> characterStore;
    static Random random =  new Random();
    static string hashChars = "abcdefghijklmnopqrstuvwxyz1234567890";
    const int hashLength = 5;
    
    public CharacterStoreMock()
    {
        
        characterStore = new Dictionary<string, Character>();
        characterStore.Add("a",new Character("Test Man",0));
        
    }

    public String CreateCharacter(Character character)
    {
        String characterHash = RandomString(hashLength);
        while (characterStore.ContainsKey(characterHash))
        {
            characterHash = RandomString(hashLength);
        }
        characterStore.Add(characterHash,character);
        return characterHash;
    }

    public void DeleteCharacter(string charID)
    {
        GetCharacter(charID);
        characterStore.Remove(charID);
    }

    public Character GetCharacter(string charID)
    {
        Character character;
        if(characterStore.TryGetValue(charID,out character))
        {
            return character;
        }
        throw new KeyNotFoundException("The character with ID " + charID +" does not exist.");
    }

    public Character UpdateCharacter(Character updatedChar, string charID)
    {
        if (characterStore.TryGetValue(charID,out _))
        {
            characterStore[charID] = updatedChar;
            return updatedChar;
        }
        throw new KeyNotFoundException("The character with ID " + charID +" does not exist.");
    }

    public static string RandomString(int length)
    {
    return new string(Enumerable.Repeat(hashChars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}