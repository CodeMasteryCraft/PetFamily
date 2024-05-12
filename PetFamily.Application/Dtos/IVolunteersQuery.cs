using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Dtos;

public interface IVolunteersQuery
{
    Task<Result<VolunteerDto, Error>> GetById(Guid id, CancellationToken ct);
}