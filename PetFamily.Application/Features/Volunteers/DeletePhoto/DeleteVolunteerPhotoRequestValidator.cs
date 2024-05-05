using FluentValidation;
using PetFamily.Application.CommonValidators;

namespace PetFamily.Application.Features.Volunteers.DeletePhoto;

public class DeleteVolunteerPhotoRequestValidator : AbstractValidator<DeleteVolunteerPhotoRequest>
{
    public DeleteVolunteerPhotoRequestValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmptyWithError();

        RuleFor(x => x.Path).NotEmptyWithError();
    }
}