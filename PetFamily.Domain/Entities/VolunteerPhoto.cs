using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class VolunteerPhoto : Entity
{
    private VolunteerPhoto()
    {
    }

    public VolunteerPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; private set; }
    public bool IsMain { get; private set; }

    public static Result<VolunteerPhoto, Error> CreateAndActivate(string path)
    {
        return new VolunteerPhoto(path, true);
    }
}