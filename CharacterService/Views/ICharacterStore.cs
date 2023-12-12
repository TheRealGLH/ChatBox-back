namespace CharacterService.Views;

using CharacterService.Messaging;
using CharacterService.Models;

public interface ICharacterStore
{
    public void registerMessager(IRabitMQProducer producer);
    public Character CreateCharacter(Character character);
    public Character GetCharacter(String charID);
    public List<Character> GetAllUserCharacters(String uuid);
    public Character UpdateCharacter(Character character, String charID);
    public void DeleteCharacter(String charID);

}