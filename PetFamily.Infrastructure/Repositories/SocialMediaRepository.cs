using CSharpFunctionalExtensions;
using PetFamily.Application.Features.SocialMedias;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class SocialMediaRepository : ISocialMediaRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public SocialMediaRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<SocialMedia, Error>> GetById(Guid id)
    {
        var sm = await _dbContext.SocialMedias.FindAsync(id);

        if (sm is null)
            return Errors.General.NotFound(id);

        return sm;
    }
}