using PetFamily.API.Contracts;
using PetFamily.Application.Models;

namespace PetFamily.API.Mapping
{
    //This class needed for Map requests from clients with Pet commands models
    public class PetMap
    {
        /// <summary>
        /// Map the Create request with the Create model
        /// </summary>
        /// <param name="request">Request from clinet</param>
        /// <returns>Model for create Pet object</returns>
        public CreatePetModel MapWith(CreatePetRequest request) =>
             new CreatePetModel(
            request.Nickname,
            request.Description,
            request.BirthDate,
            request.Breed,
            request.Color,
            request.City,
            request.Street,
            request.Building,
            request.Index,
            request.Place,
            request.Castration,
            request.PeopleAttitude,
            request.AnimalAttitude,
            request.OnlyOneInFamily,
            request.Health,
            request.Height,
            request.Weight,
            request.Vaccine,
            request.ContactPhoneNumber,
            request.VolunteerPhoneNumber,
            request.OnTreatment,
            DateTime.Now
            );
    }
}
