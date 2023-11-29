using CharacterService.Models;
using CharacterService.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CharacterService.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{

    private readonly ILogger<CharacterController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;
    private static ICharacterStore characterStore;

    public CharacterController(ILogger<CharacterController> logger, IOptions<CharacterDatabaseSettings> characterDatabaseSettings,
        IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        string DatabaseEnvironmentMock;
        bool toDelete = false;


        /*
        DatabaseEnvironmentMock = Environment.GetEnvironmentVariable("DATABASE_MOCK");
        // If necessary, create it.
        if (DatabaseEnvironmentMock == null || DatabaseEnvironmentMock == "true")
        {
            characterStore = new CharacterStoreMock();
            Console.WriteLine("Database mock type not set or 'true'. Not connecting to DB provider");
        }
        else
        {*/
        characterStore = new CharacterStoreDatabase(characterDatabaseSettings);
        //}
    }

    [HttpGet]
    [Route("{characterID}")]
    [ProducesResponseType(typeof(Character), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Get(String characterID)
    {
        Character character;
        try
        {
            character = characterStore.GetCharacter(characterID);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("The character with ID: " + characterID + " does not exist.");
        }
        var result = _authorizationService.AuthorizeAsync(User, character, "GetPolicy");
        if (result.IsCompletedSuccessfully) return Ok(character);
        else if (User.Identity.IsAuthenticated)
        {
            return new ForbidResult();
        }
        else
        {
            return new ChallengeResult();
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
        catch (KeyNotFoundException e)
        {
            return NotFound("The character with ID: " + characterID + " does not exist.");
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
            return Ok(characterStore.UpdateCharacter(character, characterID));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("The character with ID: " + characterID + " does not exist.");
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Create(Character character)
    {
        if(User.Identity.IsAuthenticated) return Ok(characterStore.CreateCharacter(character));
        return new ForbidResult();
    }
}
