using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class Photo : Entity
{
    private Photo()
    {
    }

    public Photo(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; private set; }
    public bool IsMain { get; private set; }

    public static Result<Photo, Error> CreateAndActivate(string path, string contentType)
    {
        string[] allowedContentTypes = { "image/jpeg", "image/png", "image/png" };
        if (!allowedContentTypes.Contains(contentType))
            return Errors.Volunteers.FileTypeInvalid(contentType);
        return new Photo(path, true);
    }
}