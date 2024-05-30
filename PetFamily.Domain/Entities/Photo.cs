using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public abstract class Photo : Entity
{
    private const string JPEG = "image/jpeg";
    private const string JPG = "image/jpg";
    private const string PNG = "image/png";
    protected Photo(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; protected set; }
    public bool IsMain { get; protected set; }
    public static Result<VolunteerPhoto, Error> CreateAndActivate(string path, string contentType, long length, bool isMain)
    {
        if (contentType != JPG && contentType != JPEG && contentType != PNG)
            return Errors.Volunteers.FileTypeInvalid(contentType);
        if (length > 10000)
            return Errors.Volunteers.FileLengthInvalid(length);
        return new VolunteerPhoto(path, isMain);
    }
}
