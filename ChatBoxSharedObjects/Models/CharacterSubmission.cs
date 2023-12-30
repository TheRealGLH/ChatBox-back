namespace CharacterService.Models;

public class CharacterSubmission
{
    private const int NAME_MIN_LENGTH = 2;
    private const int NAME_MAX_LENGTH = 25;
    private const int PRONOUN_MAX_LENGTH = 15;
    private const int SPECIES_MAX_LENGTH = 15;
    private const int BIO_MAX_LENGTH = 1024;


    public string characterName { get; set; }

    public String Pronouns { get; set; }
    public String Species { get; set; }
    public String Bio { get; set; }


    public CharacterSubmissionValidationState validateSubmission()
    {
        if (string.IsNullOrWhiteSpace(characterName) | characterName.Length < NAME_MIN_LENGTH) return CharacterSubmissionValidationState.NameTooShort;
        if (characterName.Length > NAME_MAX_LENGTH) return CharacterSubmissionValidationState.NameTooLong;
        if (Bio.Length > BIO_MAX_LENGTH) return CharacterSubmissionValidationState.BioTooLong;
        if (Pronouns.Length > PRONOUN_MAX_LENGTH) return CharacterSubmissionValidationState.PronounsTooLong;
        if (Species.Length > SPECIES_MAX_LENGTH) return CharacterSubmissionValidationState.SpeciesTooLong;

        return CharacterSubmissionValidationState.Ok;
    }

}

public enum CharacterSubmissionValidationState
{
    Ok,
    NameTooShort,
    NameTooLong,
    PronounsTooLong,
    SpeciesTooLong,
    BioTooLong

}