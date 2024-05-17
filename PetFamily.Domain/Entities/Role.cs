using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Role : Entity
{
    public static readonly Role Admin = new(
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

    private Role(string name, string[] permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    public string Name { get; private set; }

    public string[] Permissions { get; private set; }
}