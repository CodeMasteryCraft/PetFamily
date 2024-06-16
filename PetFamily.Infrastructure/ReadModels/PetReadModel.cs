using PetFamily.Domain.ValueObjects;

namespace PetFamily.Infrastructure.ReadModels;

public class PetReadModel
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string Nickname { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Breed { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public string PeopleAttitude { get; init; } = string.Empty;
    public string AnimalAttitude { get; init; } = string.Empty;
    public string Health { get; init; } = string.Empty;

    public Address Address { get; private set; } = null!;
    // public Place Place { get; private set; } = null!;
    // public Weight Weight { get; private set; } = null!;
    // public PhoneNumber ContactPhoneNumber { get; private set; } = null!;
    // public PhoneNumber VolunteerPhoneNumber { get; private set; } = null!;

    public bool Castration { get; init; }
    public bool OnlyOneInFamily { get; init; }
    public bool OnTreatment { get; init; }
    public int? Height { get; init; }
    public DateTimeOffset BirthDate { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}