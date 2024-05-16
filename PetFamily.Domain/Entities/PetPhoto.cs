using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class PetPhoto : Entity
{
    private PetPhoto()
    {
    }

    public PetPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; private set; }
    public bool IsMain { get; private set; }

    public static Result<PetPhoto, Error> CreateAndActivate(string path)
    {
        return new PetPhoto(path, true);
    }
}