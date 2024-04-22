using Contracts.Pets.Dtos;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Extensions;

public static class MappingExtensions
{
    public static PetDto ToDto(this Pet pet)
    {
        return new(
            pet.Id,
            pet.Nickname,
            pet.Description,
            pet.Address.City,
            pet.Address.Street,
            pet.Address.Building,
            pet.Address.Index,
            pet.ContactPhoneNumber.Number);
    }
}