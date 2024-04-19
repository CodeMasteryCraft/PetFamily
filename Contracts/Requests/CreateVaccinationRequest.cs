namespace Contracts.Requests;

public record CreateVaccinationRequest(
    string Name,
    DateTimeOffset Applied
    );
