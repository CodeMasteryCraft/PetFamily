using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.SocialMedias;

public interface ISocialMediasRepository
{
    Task<Result<SocialMedia, Error>> GetById(Guid id);
}