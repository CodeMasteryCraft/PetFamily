using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Abstractions;

public interface IPetFamilyReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}