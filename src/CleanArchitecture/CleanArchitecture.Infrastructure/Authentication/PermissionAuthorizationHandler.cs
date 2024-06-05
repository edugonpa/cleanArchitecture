using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Authentication;

public class PermissionAuthorizationHandler 
: AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier
        )?.Value;
        
        if (userId == null)
        {
            return Task.CompletedTask;
        }

        HashSet<string> permissions = context.User.Claims
            .Where(x => x.Type == CustomClaims.Permission)
            .Select(x => x.Value).ToHashSet();

        if(permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}