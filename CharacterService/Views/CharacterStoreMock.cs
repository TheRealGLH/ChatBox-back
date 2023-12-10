using CharacterService.Models;
using CharacterService.Views;


class CharacterStoreMock : ICharacterStore
{
    Dictionary<String, Character> characterStore;
    static Random random = new Random();
    static string hashChars = "abcdefghijklmnopqrstuvwxyz1234567890";
    const int hashLength = 5;

    public CharacterStoreMock()
    {

        characterStore = new Dictionary<string, Character>();
        characterStore.Add("a", new Character("Test Man", "he/him","human","A man with mysterious powers."));

    }

    public Character CreateCharacter(Character character)
    {
        String characterHash = RandomString(hashLength);
        while (characterStore.ContainsKey(characterHash))
        {
            characterHash = RandomString(hashLength);
        }
        characterStore.Add(characterHash, character);
        return character;
    }

    public void DeleteCharacter(string charID)
    {
        GetCharacter(charID);
        characterStore.Remove(charID);
    }

    public Character GetCharacter(string charID)
    {
        Character character;
        if (characterStore.TryGetValue(charID, out character))
        {
            return character;
        }
        throw new KeyNotFoundException("The character with ID " + charID + " does not exist.");
    }

    public Character UpdateCharacter(Character updatedChar, string charID)
    {
        Character oldCharacter = GetCharacter(charID);
        if (oldCharacter != updatedChar)
        {
            characterStore[charID] = updatedChar;
        }
        return updatedChar;
    }

    static string RandomString(int length)
    {
        return new string(Enumerable.Repeat(hashChars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public List<Character> GetAllUserCharacters(string uuid)
    {
        throw new NotImplementedException("Method GetAllUserCharacters() not yet implemented");
    }
}