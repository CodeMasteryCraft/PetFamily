using FluentValidation;
using LanguageExt.Common;
using PetFamily.Application.Interfaces;
using PetFamily.Application.Models;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Pets.Services
{
    public class PetService : IPetService
    {
        private readonly IValidator<Pet> _validator;
        private readonly IPetRepository _repository;
        public PetService(IPetRepository petRepository, IValidator<Pet> validator)
        {
            _validator = validator;
            _repository = petRepository;
        }

        /// <summary>
        /// Create a new Pet object
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id of Pet object</returns>
        public async Task<Result<Guid>> CreateAsync(CreatePetModel model, CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();

            var pet = new Pet(id,
                model.Nickname,
                model.Description, model.BirthDate,
                model.Breed,  model.Color,
                new Address(model.City, model.Street, model.Building, model.Index),
                Place.Create(model.Place),
                model.Castration, model.PeopleAttitude,
                model.AnimalAttitude, model.OnlyOneInFamily,
                model.Health, model.Height,
                new Weight(model.Weight), model.Vaccine,
                PhoneNumber.Create(model.ContactPhoneNumber),
                PhoneNumber.Create(model.VolunteerPhoneNumber),
                model.OnTreatment, model.CreatedDate);

            var resultValidation = await _validator.ValidateAsync(pet);
            if (!resultValidation.IsValid)
            {
                var error = new ValidationException(resultValidation.Errors);
                return new Result<Guid>(error);
            }

            await _repository.AddAsync(pet, cancellationToken);

            return id;
        }
    }
}
