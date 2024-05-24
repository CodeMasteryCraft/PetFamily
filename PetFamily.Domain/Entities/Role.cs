using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Role : Entity
{
    public static readonly Role Admin = new(
        Guid.NewGuid(),
        nameof(Admin).ToUpper(),
        [
            Common.Permissions.VolunteerApplications.Update,

            Common.Permissions.Pets.Read,
            Common.Permissions.Pets.Delete,

            Common.Permissions.Volunteers.Create,
            Common.Permissions.Volunteers.Delete,
            Common.Permissions.Volunteers.Read,
        ]);

    public static readonly Role Volunteer = new(
        Guid.NewGuid(),
        nameof(Volunteer).ToUpper(),
        [
            Common.Permissions.Pets.Read,
            Common.Permissions.Pets.Create,
            Common.Permissions.Pets.Update,
            Common.Permissions.Pets.Delete,

            Common.Permissions.Volunteers.Read,
        ]);

    private Role()
    {
    }

    private Role(Guid id, string name, string[] permissions)
        : base(id)
    {
        Name = name;
        Permissions = permissions;
    }

    public string Name { get; private set; }

    public string[] Permissions { get; private set; }
}