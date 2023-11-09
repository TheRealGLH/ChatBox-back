namespace CharacterService.Controllers;

public class Character
{
    public String CharacterName { get; set; }
    public Gender Gender { get; set; }
    public Character(String CharacterName, Gender gender)
    {
        this.CharacterName = CharacterName;
        this.Gender = gender;
    }
}