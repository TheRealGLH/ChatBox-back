using System.Security.Claims;
using CharacterService.Models;
using Microsoft.AspNetCore.Authorization;

public class AccountAuthorizationHandler :
    AuthorizationHandler<HasCorrectRoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   HasCorrectRoleRequirement requirement)
    {
        //TODO If we implement custom roles, do the check here.
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}

public class HasCorrectRoleRequirement : IAuthorizationRequirement { }

