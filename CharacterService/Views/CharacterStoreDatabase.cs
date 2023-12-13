namespace CharacterService.Views;

using System.Collections.Generic;
using CharacterService.Connectors;
using CharacterService.Messaging;
using CharacterService.Models;
using Microsoft.Extensions.Options;
using ChatBoxSharedObjects.Messages;
using ChatBoxSharedObjects.Security;
using ChatBoxSharedObjects.Settings;

public class CharacterStoreDatabase : ICharacterStore
{
    ICharacterDatabaseConnector characterConnector;
    IRabbitMqProducer rabitMQProducer;

    public CharacterStoreDatabase(IOptions<MongoDatabaseSettings> characterDatabaseSettings)
    {
        characterConnector = new CharacterDatabaseConnectorMongo(characterDatabaseSettings);
    }
    public Character CreateCharacter(Character character)
    {
        Character charAdded = characterConnector.Add(character);
        rabitMQProducer.SendCreationMessage(
            new CharacterMessage(charAdded.Id, charAdded.owner, CharacterMessageType.CREATE
            ));
        return charAdded;
    }

    public void DeleteCharacter(string characterId)
    {
        GetCharacter(characterId);
        characterConnector.Delete(characterId);
        rabitMQProducer.SendCreationMessage(
    new CharacterMessage(characterId, "none", CharacterMessageType.DELETE
    ));
    }

    public List<Character> GetAllUserCharacters(string uuid)
    {
        return characterConnector.GetAllUserCharacters(uuid);
    }

    public Character GetCharacter(string characterId)
    {
        Character character = characterConnector.Get(characterId);
        if (character != null) return character;
        throw new KeyNotFoundException("The character with ID " + characterId + " does not exist.");
    }

    public void registerMessager(IRabbitMqProducer producer)
    {
        this.rabitMQProducer = producer;
    }

    public Character UpdateCharacter(Character updatedChar, string charID)
    {
        Character oldCharacter = GetCharacter(charID);
        if (oldCharacter != updatedChar)
        {
            characterConnector.Update(charID, updatedChar);
        }
        return updatedChar;
    }
}