using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Role : ValueObject
{
    public static readonly Role Admin = new(
        "ADMIN",
        [
            Common.Permissions.VolunteerApplications.Update,

            Common.Permissions.Pets.Read,
            Common.Permissions.Pets.Delete,

            Common.Permissions.Volunteers.Create,
            Common.Permissions.Volunteers.Delete,
            Common.Permissions.Volunteers.Read,
        ]);

    public static readonly Role Volunteer = new(
        "VOLUNTEER",
        [
            Common.Permissions.Pets.Read,
            Common.Permissions.Pets.Create,
            Common.Permissions.Pets.Update,
            Common.Permissions.Pets.Delete,

            Common.Permissions.Volunteers.Read,
        ]);

    public static readonly Role RegularUser = new(
        "REGULARUSER",
        [
            Common.Permissions.Pets.Read,
            Common.Permissions.Volunteers.Read,
        ]);

    private Role(string name, string[] permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    public string Name { get; }
    public string[] Permissions { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}