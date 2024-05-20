using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.Volunteers;

public interface IVolunteersRepository
{
    Task Add(Volunteer volunteer, CancellationToken ct);
    Task<Result<Volunteer, Error>> GetById(Guid id, CancellationToken ct);
    Task<Result<int, Error>> Save(CancellationToken ct);
    Task<Result<List<Volunteer>, Error>> GetAll(int size, int page, CancellationToken ct);
}