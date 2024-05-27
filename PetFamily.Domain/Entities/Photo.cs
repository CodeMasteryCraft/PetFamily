using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public abstract class Photo : Entity
{
    protected Photo(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; protected set; }
    public bool IsMain { get; protected set; }
    public static Result<VolunteerPhoto, Error> CreateAndActivate(string path, string contentType, long length, bool isMain)
    {
        string[] allowedContentTypes = ["image/jpeg", "image/png", "image/png"];
        if (!allowedContentTypes.Contains(contentType))
            return Errors.Volunteers.FileTypeInvalid(contentType);
        if (length > 10000)
            return Errors.Volunteers.FileLengthInvalid(length);
        return new VolunteerPhoto(path, isMain);
    }
}
