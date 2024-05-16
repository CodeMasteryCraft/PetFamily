using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Role : Entity
{
    public static Role Admin = new(
        nameof(Admin).ToUpper(),
        [
            "volunteer.applications.read",
            "volunteer.applications.update",

            "volunteers.create",

            "pets.read",
            "pets.delete",

            "volunteers.read",
            "volunteers.delete"
        ]);

    public static Role Volunteer = new(
        nameof(Volunteer).ToUpper(),
        [
            "pets.read",
            "pets.create",
            "pets.update",
            "pets.delete",

            "volunteers.read"
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