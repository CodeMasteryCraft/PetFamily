using FluentValidation;
using PetFamily.Application.CommonValidators;

namespace PetFamily.Application.Features.Volunteers.GetPhotos;

public class GetVolunteerPhotoRequestValidator : AbstractValidator<GetVolunteerPhotoRequest>
{
    public GetVolunteerPhotoRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmptyWithError();

        RuleFor(x => x.Photo).NotEmptyWithError();
    }
}
