using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly PetFamilyWriteDbContext _dbContext;

    public PhotoRepository(PetFamilyWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Photo, Error>> GetById(Guid id, CancellationToken ct)
    {
        var photo = await _dbContext.Photo.FindAsync(id);

        if (photo is null)
            return Errors.General.NotFound(id);

        return photo;
    }
}