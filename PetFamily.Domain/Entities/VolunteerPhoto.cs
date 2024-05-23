using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class VolunteerPhoto : Photo
{
    public VolunteerPhoto(string path, bool isMain) : base(path, isMain)
    {
       
    }
    
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