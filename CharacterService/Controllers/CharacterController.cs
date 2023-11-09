using Microsoft.AspNetCore.Mvc;

namespace CharacterService.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{

    private readonly ILogger<CharacterController> _logger;
    private static ICharacterStore characterStore = new CharacterStoreMock();

    public CharacterController(ILogger<CharacterController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{characterID}")]
    public Character Get(String characterID)
    {
        return characterStore.GetCharacter(characterID);
    }

    [HttpDelete]
    [Route("{characterID}")]
    public void Delete(String characterID)
    {
        characterStore.DeleteCharacter(characterID);
    }

    [HttpPatch]
    [Route("{characterID}")]
    public Character Update(String characterID, Character character)
    {
        return characterStore.UpdateCharacter(character,characterID);
    }

    [HttpPut]
    public String Create(Character character)
    {
        return characterStore.CreateCharacter(character);
    }
}
