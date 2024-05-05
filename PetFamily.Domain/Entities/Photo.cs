using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class Photo : Entity
{
    public Photo(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; private set; }
    public bool IsMain { get; private set; }

    public static Result<Photo, ResultEvent> CreateAndActivate(string path)
    {
        if(path.IsEmpty())
            return Errors.General.ValueIsInvalid();

        return new Photo(path, true);
    }
}