using PetFamily.Domain.Entities;

namespace PetFamily.Application.Interfaces
{
    //This interface is needed to interact with Database
    public interface IPetRepository
    {
        /// <summary>
        /// Add a new pet object to DataBase
        /// </summary>
        /// <param name="pet">This object add to DataBase</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(Pet pet, CancellationToken cancellationToken);
    }
}
