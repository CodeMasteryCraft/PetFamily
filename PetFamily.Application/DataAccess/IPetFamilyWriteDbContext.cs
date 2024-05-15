using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.DataAccess;

public interface IPetFamilyWriteDbContext
{
    DbSet<Volunteer> Volunteers { get; }
    DbSet<VolunteerApplication> VolunteersApplications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}