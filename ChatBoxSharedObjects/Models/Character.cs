namespace ChatBoxSharedObjects.Models;

public class Character: StoredResource
{
    public String CharacterName { get; set; }
    public String Species { get; set; }
    public String Pronouns { get; set; }
    public String Bio { get; set; }

    public Character(CharacterSubmission characterSubmission, String owner): base (owner)
    {
        this.CharacterName = characterSubmission.characterName;
        this.Pronouns = characterSubmission.Pronouns;
        this.Species = characterSubmission.Species;
        this.Bio = characterSubmission.Bio;
        this.owner = owner;
    }

    public void Update(CharacterSubmission characterSubmission)
    {
        this.CharacterName = characterSubmission.characterName;
        this.Pronouns = characterSubmission.Pronouns;
        this.Species = characterSubmission.Species;
        this.Bio = characterSubmission.Bio;
    }

}