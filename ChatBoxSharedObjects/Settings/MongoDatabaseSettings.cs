namespace ChatBoxSharedObjects.Settings;

public class MongoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CharacterCollectionName { get; set; } = null!;
}