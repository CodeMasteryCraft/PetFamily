using FluentValidation;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public class UploadPhotoValidator : AbstractValidator<UploadVolunteerPhotoRequest>
{
     public UploadPhotoValidator()
     {
         var type = string.Empty;
         long length = 0;
         
         RuleFor(p => p.File).Must(p =>
             {
                 type = p.ContentType;
                 return CheckTypes(p.ContentType);
             })
             .WithError(Errors.Volunteers.FileTypeInvalid(type));
         
         RuleFor(p => p.File).Must(p =>
             {
                 length = p.Length;
                 return CheckLength(p.Length);
             })
             .WithError(Errors.Volunteers.FileLengthInvalid(length));
     }
 
     private bool CheckTypes(string contentType)
     {
         string[] allowedContentTypes = { "image/jpeg", "image/png", "image/png" };
 
         return allowedContentTypes.Contains(contentType);
     }

     private bool CheckLength(long length)
     {
         return length < 10000;
     }
 }