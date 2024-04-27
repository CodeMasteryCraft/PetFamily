using CSharpFunctionalExtensions;
using PetFamily.Application.Features.SocialMedias;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class SocialMediaRepository : ISocialMediasRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public SocialMediaRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<SocialMedia, Error>> GetById(Guid id)
    {
        var pet = await _dbContext.SocialMedias.FindAsync(id);

        if (pet is null)
            return Errors.General.NotFound(id);

        return pet;
    }
}