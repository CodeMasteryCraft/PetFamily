namespace Contracts.Pets.Requests;

public record CreatePetRequest(
    Guid VolunteerId,
    string Nickname,
    string Color,
    string City,
    string Street,
    string Building,
    string Index,
    string Place,
    float Weight,
    bool OnlyOneInFamily,
    string Health,
    string ContactPhoneNumber,
    string VolunteerPhoneNumber,
    bool onTreatment);