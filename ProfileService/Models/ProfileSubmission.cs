namespace ProfileService.Models;

public class ProfileSubmission
{
    private const int PROFILE_MAX_LENGTH = 4096;
    private const int AGE_DESC_MAX_LENGTH = 256;
    private const int HEIGHT_MAX_LENGTH = 256;
    private const int OCCUPATION_MAX_LENGTH = 256;
    private const int PERSONALITY_MAX_LENGTH = 256;
    private const int LOCATION_MAX_LENGTH = 256;
    public string ProfileText { get; set; }
    public uint Age { get; set; }
    public string AgeDescription { get; set; }
    public string Height { get; set; }
    public string Occupation { get; set; }
    public string PersonalityDescription { get; set; }
    public string Location { get; set; }


    public ProfileValidationState validateSubmission()
    {
        if (ProfileText.Length > PROFILE_MAX_LENGTH) return ProfileValidationState.ProfileTextTooLong;
        if (AgeDescription.Length > AGE_DESC_MAX_LENGTH) return ProfileValidationState.AgeDescriptionTextTooLong;
        if (Height.Length > HEIGHT_MAX_LENGTH) return ProfileValidationState.HeightTextTooLong;
        if (Occupation.Length > OCCUPATION_MAX_LENGTH) return ProfileValidationState.OccupationTextTooLong;
        if (PersonalityDescription.Length > PERSONALITY_MAX_LENGTH) return ProfileValidationState.PersonalityDescriptionTextTooLong;
        if (Location.Length > LOCATION_MAX_LENGTH) return ProfileValidationState.LocationTextTooLong;
        return ProfileValidationState.Ok;
    }
}


public enum ProfileValidationState
{
    Ok,
    ProfileTextTooLong,
    AgeInvalid,
    AgeDescriptionTextTooLong,
    HeightTextTooLong,
    OccupationTextTooLong,
    PersonalityDescriptionTextTooLong,
    LocationTextTooLong
}