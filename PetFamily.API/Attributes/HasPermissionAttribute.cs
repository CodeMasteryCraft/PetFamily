using Microsoft.AspNetCore.Authorization;

namespace PetFamily.API.Attributes;

public class HasPermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string Permission { get; }

    public HasPermissionAttribute(string permission)
        : base(policy: permission)
    {
        Permission = permission;
    }
}