using PetFamily.Application.Interfaces;
using PetFamily.Application.Models;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Pets.Services
{
    public class PetService(IPetRepository repository) : IPetService
    {
        private readonly IPetRepository _repository = repository;

        /// <summary>
        /// Create a new Pet object
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreatePetModel model, CancellationToken cancellationToken)
        {
            var pet = new Pet(model.Nickname,
                model.Description, model.BirthDate,
                model.Breed,  model.Color,
                model.Address, model.Place,
                model.Castration, model.PeopleAttitude,
                model.AnimalAttitude, model.OnlyOneInFamily,
                model.Health, model.Height,
                model.Weight, model.Vaccine,
                model.ContactPhoneNumber, model.VolunteerPhoneNumber,
                model.OnTreatment, model.CreatedDate);

            await _repository.AddAsync(pet, cancellationToken);
        }
    }
}
