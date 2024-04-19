using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Photo
{   
    private Photo(Guid id,string path, bool isMain)
    {
        Id = id;
        Path = path;
        IsMain = isMain;
    }

    public Guid Id { get; private set; }

    public string Path { get; private set; }

    public bool IsMain { get; private set; }


    public static Result<Photo, Error> Create(Guid id,string path, bool isMain)
    {
        if (path.IsEmpty())
            return Errors.General.ValueIsRequired();

        return new Photo(id, path, isMain);
    }
}