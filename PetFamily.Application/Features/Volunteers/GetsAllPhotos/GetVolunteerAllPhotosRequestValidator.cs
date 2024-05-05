using FluentValidation;
using PetFamily.Application.CommonValidators;

namespace PetFamily.Application.Features.Volunteers.GetsPhotos;

public class GetVolunteerAllPhotosRequestValidator : AbstractValidator<GetVolunteerAllPhotosRequest>
{
    public GetVolunteerAllPhotosRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmptyWithError();
    }
}
