using PetFamily.Domain.Common;

namespace PetFamily.Application.Dtos;

public class SocialMediaDto : Entity
{
    public string Link { get; init; } = string.Empty;
    public string Social { get; init; } = string.Empty;
}