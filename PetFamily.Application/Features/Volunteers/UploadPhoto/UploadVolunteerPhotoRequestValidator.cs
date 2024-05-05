using FluentValidation;
using PetFamily.Application.CommonValidators;

namespace PetFamily.Application.Features.Volunteers.UploadPhoto;

public class UploadVolunteerPhotoRequestValidator : AbstractValidator<UploadVolunteerPhotoRequest>
{
    public UploadVolunteerPhotoRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmptyWithError();
        
        RuleFor(x => x.File).NotEmptyWithError();
    }
}
