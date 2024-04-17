using PetFamily.Application.Interfaces;
using PetFamily.Domain.Entities;

namespace PetFamily.Infrastructure.Respositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetFamilyDbContext _context;
        public PetRepository(PetFamilyDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Pet pet, CancellationToken cancellationToken)
        {
            await _context.AddAsync(pet, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
