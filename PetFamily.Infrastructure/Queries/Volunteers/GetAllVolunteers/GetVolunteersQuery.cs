using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Queries.Volunteers.GetAllVolunteers
{
    public class GetVolunteersQuery
    {
        private readonly PetFamilyReadDbContext _readDbContext;

        public GetVolunteersQuery(PetFamilyReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<GetVolunteersResponse, Error>> Handle(GetVolunteersRequest request, CancellationToken ct)
        {
            var volunteers = await _readDbContext.Volunteers
                .Include(v => v.Pets)
                .Include(v => v.Photos)
                .Skip(request.Size * (request.Page - 1))
                .Take(request.Size)
                .ToListAsync(cancellationToken: ct);


            var dtoList = volunteers.Select(v => new VolunteerDto(
                v.Id,
                v.FirstName,
                v.LastName,
                v.Patronymic,
                v.Photos.Select(x => new VolunteerPhotoDto { Id = x.Id, Path = x.Path, IsMain = x.IsMain })
                    .ToList())).ToList();

            return new GetVolunteersResponse(dtoList);
        }
    }
}