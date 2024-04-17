using FluentValidation;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Common.Validations
{
    public class PetValidator : AbstractValidator<Pet>
    {
        public PetValidator()
        {
            //Base properties

            RuleFor(p => p.Nickname).NotEmpty().WithMessage("Nickname can not be empty");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description can not be empty");
            RuleFor(p => p.BirthDate).NotEmpty().WithMessage("Birth date can not be empty");
            RuleFor(p => p.Breed).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Color).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Castration).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.PeopleAttitude).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.AnimalAttitude).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.OnlyOneInFamily).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Health).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Vaccine).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.OnTreatment).NotEmpty().WithMessage("Street can not be empty");

            //Values objects properties

            //Adress Validation
            RuleFor(p => p.Address.Street).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Address.City).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Address.Building).NotEmpty().WithMessage("Street can not be empty");
            RuleFor(p => p.Address.Index).NotEmpty().WithMessage("Street can not be empty");
            
            //Weight Validation
            RuleFor(p => p.Weight.Grams).NotEmpty().WithMessage("Weight can not be empty");

            //Contact phone Validation
            RuleFor(p => p.ContactPhoneNumber.Number).NotEmpty().WithMessage("Contact phone number can not be empty"); 
            
            //Volunteer phone Validation
            RuleFor(p => p.VolunteerPhoneNumber.Number).NotEmpty().WithMessage("Volunteer phone number can not be empty"); 

            //Place Validation
            RuleFor(p => p.Place.Value).NotEmpty().WithMessage("Place can not be empty");

        }
    }
}
