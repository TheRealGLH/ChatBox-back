using CharacterService.Models;
using Microsoft.AspNetCore.Authorization;

public class DocumentAuthorizationHandler : 
    AuthorizationHandler<SameAuthorRequirement, Character>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SameAuthorRequirement requirement,
                                                   Character resource)
    {
        if (context.User.Identity?.Name == resource.owner)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class SameAuthorRequirement : IAuthorizationRequirement { }