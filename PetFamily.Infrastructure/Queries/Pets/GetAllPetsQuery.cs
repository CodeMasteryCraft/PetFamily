using Dapper;
using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Pets.GetPets;

namespace PetFamily.Infrastructure.Queries.Pets;

public class GetAllPetsQuery
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;

    public GetAllPetsQuery(SqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<GetPetsResponse> Handle()
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = """
                   SELECT p.id,
                   	p.nickname,
                   	p.breed,
                   	p.contact_phone_number,
                   	ph.id,
                   	ph.path,
                   	ph.is_main
                   FROM pets p
                   LEFT JOIN photos ph ON p.id = ph.pet_id
                   """;

        Dictionary<Guid, PetDto> petsDictionary = new();
        await connection.QueryAsync<PetDto, PetPhotoDto, PetDto>(
            sql,
            (pet, photo) =>
            {
                if (petsDictionary.TryGetValue(pet.Id, out var existingPet))
                {
                    pet = existingPet;
                }
                else
                {
                    petsDictionary.Add(pet.Id, pet);
                }
                //TODO проблема с IReadOnlyList
                //pet.Photos.Add(photo);
                return pet;
            },
            splitOn: "id");

        return new(petsDictionary.Select(p => p.Value), 10);
    }
}