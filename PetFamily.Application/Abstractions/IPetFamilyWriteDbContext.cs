using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Abstractions;

public interface IPetFamilyWriteDbContext
{
    DbSet<Volunteer> Volunteers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}