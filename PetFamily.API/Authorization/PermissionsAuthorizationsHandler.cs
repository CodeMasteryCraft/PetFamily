using Microsoft.AspNetCore.Authorization;
using PetFamily.API.Attributes;
using PetFamily.Infrastructure.Constants;

namespace PetFamily.API.Authorization;

public class PermissionsAuthorizationsHandler : AuthorizationHandler<HasPermissionAttribute>
{
    private readonly ILogger<PermissionsAuthorizationsHandler> _logger;

    public PermissionsAuthorizationsHandler(ILogger<PermissionsAuthorizationsHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, HasPermissionAttribute requirement)
    {
        var permissions = context.User.Claims
            .Where(c => c.Type == Constants.Authentication.Permissions)
            .Select(c => c.Value);

        if (!permissions.Contains(requirement.Permission))
        {
            _logger.LogError("User has not permission: {permission}", requirement.Permission);
            return Task.CompletedTask;
        }

        _logger.LogInformation("User has permission: {permission}", requirement.Permission);
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}