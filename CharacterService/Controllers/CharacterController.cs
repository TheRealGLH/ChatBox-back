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
    [ProducesResponseType(typeof(Character), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Get(String characterID)
    {
        try 
        {
            return Ok(characterStore.GetCharacter(characterID));
        }
        catch(KeyNotFoundException e)
        {
            return NotFound("The character with ID: "+characterID+" does not exist.");
        }
    }

    [HttpDelete]
    [Route("{characterID}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Delete(String characterID)
    {
        try 
        {
            characterStore.DeleteCharacter(characterID);
            return Ok();
        }
        catch(KeyNotFoundException e)
        {
            return NotFound("The character with ID: "+characterID+" does not exist.");
        }
    }

    [HttpPatch]
    [Route("{characterID}")]
    [ProducesResponseType(typeof(Character), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Update(String characterID, Character character)
    {
        try 
        {
            return Ok(characterStore.UpdateCharacter(character,characterID));
        }
        catch(KeyNotFoundException e)
        {
            return NotFound("The character with ID: "+characterID+" does not exist.");
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public String Create(Character character)
    {
        return characterStore.CreateCharacter(character);
    }
}
