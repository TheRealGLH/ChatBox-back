namespace CharacterService.Models;

public class CharacterDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CharacterCollectionName { get; set; } = null!;
}