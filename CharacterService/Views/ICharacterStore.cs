using CharacterService.Controllers;

public interface ICharacterStore
{
    public String CreateCharacter(Character character);
    public Character GetCharacter(String charID);
    public Character UpdateCharacter(Character character, String charID);
    public void DeleteCharacter(String charID);

}