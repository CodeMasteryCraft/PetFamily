using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers;

public interface IVolunteersRepository
{
    Task Add(Volunteer volunteer, CancellationToken ct);
    Task<Result<Volunteer, ResultEvent>> GetById(Guid id, CancellationToken ct);
    Task<Result<int, ResultEvent>> Save(CancellationToken ct);
    Task<Result<List<string>, ResultEvent>> GetByPhoto(Guid volunteerId, CancellationToken ct);
}