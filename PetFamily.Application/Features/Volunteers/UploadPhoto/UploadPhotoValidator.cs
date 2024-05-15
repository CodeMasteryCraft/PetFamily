using FluentValidation;
using Microsoft.AspNetCore.Http;
using PetFamily.Application.CommonValidators;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public class UploadPhotoValidator : AbstractValidator<UploadVolunteerPhotoRequest>
{
     public UploadPhotoValidator()
     {
         RuleFor(p => p.File).Must(p => CheckTypes(p.ContentType))
             .WithError(Errors.General.ValueIsInvalid());
         // RuleFor(p => p.File.ContentType).MustBePhoto();
     }
 
     private bool CheckTypes(string contentType)
     {
         string[] allowedContentTypes = { "image/jpeg", "image/png", "image/png" };
 
         return allowedContentTypes.Contains(contentType);
     }
 }