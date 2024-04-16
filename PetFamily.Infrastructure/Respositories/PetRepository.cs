using PetFamily.Application.Interfaces;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Respositories
{
    public class PetRepository(PetFamilyDbContext context) : IPetRepository
    {
        private readonly PetFamilyDbContext _context = context;
        public async Task AddAsync(Pet pet, CancellationToken cancellationToken)
        {
            await _context.AddAsync(pet, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
