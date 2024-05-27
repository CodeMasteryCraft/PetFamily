using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class PetPhoto : Photo
{
    public PetPhoto(string path, bool isMain) : base(path, isMain)
    {
    }
    // public static Result<PetPhoto, Error> CreateAndActivate(string path, string contentType, long length, bool isMain)
    // {
    //     string[] allowedContentTypes = ["image/jpeg", "image/png", "image/png"];
    //     if (!allowedContentTypes.Contains(contentType))
    //         return Errors.Volunteers.FileTypeInvalid(contentType);
    //     if (length > 10000)
    //         return Errors.Volunteers.FileLengthInvalid(length);
    //     return new PetPhoto(path, isMain);
    // }
}