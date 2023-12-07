using CharacterService.Controllers;

namespace CharacterService.Views;

using CharacterService.Models;

public interface ICharacterStore
{
    public Character CreateCharacter(Character character);
    public Character GetCharacter(String charID);
    public Character UpdateCharacter(Character character, String charID);
    public void DeleteCharacter(String charID);

}