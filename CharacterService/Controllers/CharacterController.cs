using System.Security.Claims;
using CharacterService.Models;
using CharacterService.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CharacterService.Controllers;

[Authorize]
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

    [HttpPost]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Create(CharacterSubmission characterSubmission)
    {
        if (User.Identity.IsAuthenticated) return Ok(characterStore.CreateCharacter(new Character(
            characterSubmission,
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        )));
        return new ForbidResult();
    }

    [HttpGet]
    [Route("{characterID}")]
    [ProducesResponseType(typeof(Character), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Read(String characterID)
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
            return new ChallengeResult("You're not authorized to view this character.");
        }

    }

    [HttpPut]
    [Route("{characterID}")]
    [ProducesResponseType(typeof(Character), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Update(String characterID, Character character)
    {
        //TODO: Character Update field, perhaps?
        try
        {
            return Ok(characterStore.UpdateCharacter(character, characterID));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("The character with ID: " + characterID + " does not exist.");
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



}
