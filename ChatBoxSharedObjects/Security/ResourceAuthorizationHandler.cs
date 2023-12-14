using System.Security.Claims;
using ChatBoxSharedObjects.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChatBoxSharedObjects.Security;

public class DocumentAuthorizationHandler :
    AuthorizationHandler<SameAuthorRequirement, StoredResource>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SameAuthorRequirement requirement,
                                                   StoredResource resource)
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

