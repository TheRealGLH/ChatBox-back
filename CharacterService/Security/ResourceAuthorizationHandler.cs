using System.Security.Claims;
using CharacterService.Models;
using Microsoft.AspNetCore.Authorization;

public class DocumentAuthorizationHandler :
    AuthorizationHandler<SameAuthorRequirement, Character>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SameAuthorRequirement requirement,
                                                   Character resource)
    {

        if (context.User.FindFirstValue("user_id") == resource.owner)
        {
            context.Succeed(requirement);
        }
        else { context.Fail(); }


        return Task.CompletedTask;
    }
}

public class SameAuthorRequirement : IAuthorizationRequirement { }

