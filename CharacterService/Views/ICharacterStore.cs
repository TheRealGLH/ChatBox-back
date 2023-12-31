
using CharacterService.Messaging;
using CharacterService.Models;
using ChatBoxSharedObjects.Models;

namespace CharacterService.Views;
public interface ICharacterStore
{
    public Character CreateCharacter(Character character);
    public Character GetCharacter(String charID);
    public List<Character> GetAllUserCharacters(String uuid);
    public Character UpdateCharacter(Character character, String charID);
    public void DeleteCharacter(String charID);

}