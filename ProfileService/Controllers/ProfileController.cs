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
}
