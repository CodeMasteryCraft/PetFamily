using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public abstract class Photo : Entity
{
    public const string JPEG = ".jpeg";
    public const string JPG = ".jpg";
    public const string PNG = ".png";

    protected Photo(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; protected set; }
    public bool IsMain { get; protected set; }
    public static Result<VolunteerPhoto> CreateAndActivate(string path, string contentType, long length, bool isMain)
    {
        if (contentType != JPG && contentType != JPEG && contentType != PNG)
            return Errors.Volunteers.FileTypeInvalid(contentType);
        if (length > 100000)
            return Errors.Volunteers.FileLengthInvalid(length);
        return new VolunteerPhoto(path, isMain);
    }
}
