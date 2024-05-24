using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.VolunteerApplications;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerApplicationsRepository : IVolunteerApplicationsRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public VolunteerApplicationsRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<VolunteerApplication, Error>> GetById(Guid id, CancellationToken ct)
    {
        var application = await _dbContext.VolunteersApplications
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: ct);

        if (application is null)
            return Errors.General.NotFound(id);

        return application;
    }

    public async Task Add(VolunteerApplication application, CancellationToken ct)
    {
        await _dbContext.VolunteersApplications.AddAsync(application, ct);
    }
}