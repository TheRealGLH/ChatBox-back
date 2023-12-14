using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Models;
using ProfileService.Views;

namespace ProfileService.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileStore _profileStore;
    private readonly ILogger<ProfileController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;


    public ProfileController(ILogger<ProfileController> logger,
    IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IProfileStore profileStore)
    {
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        this._profileStore = profileStore;
    }


    [HttpGet]
    [Route("{characterID}")]
    [ProducesResponseType(typeof(Profile), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public Profile ReadProfile(String characterID)
    {
        return _profileStore.GetProfile(characterID);
    }

    [HttpPut]
    [Route("{characterID}")]
    [ProducesResponseType(typeof(Profile), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public IActionResult Update(String characterID, ProfileSubmission profileToUpdate)
    {
        Profile profile;
        try
        {
            profile = _profileStore.GetProfile(characterID);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("The profile with ID: " + characterID + " does not exist.");
        }
        var result = _authorizationService.AuthorizeAsync(User, profile, "EditPolicy");
        if (result.Result.Succeeded)
        {
            ProfileValidationState validationState = profileToUpdate.validateSubmission();
            if (validationState == ProfileValidationState.Ok)
            {
                profile.Update(profileToUpdate);
                _profileStore.UpdateProfile(profile, characterID);
                return Ok(profile);
            }
            else return BadRequest("The request was invalid because: " + validationState.ToString());
        }
        else if (User.Identity.IsAuthenticated)
        {
            return new ForbidResult();
        }
        else
        {
            return new ChallengeResult("You're not authorized to alter this profile.");
        }
    }
}
