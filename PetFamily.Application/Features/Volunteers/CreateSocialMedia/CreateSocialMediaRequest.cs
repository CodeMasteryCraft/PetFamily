using FluentValidation;

namespace PetFamily.Application.Features.Volunteers.CreateSocialMedia;

public record CreateSocialMediaRequest(
    Guid VolunteerId,
    string Link,
    string Social);

public class CreateSocialMediaRequestValidator : AbstractValidator<CreateSocialMediaRequest>
{
    public CreateSocialMediaRequestValidator()
    {
        // написать валидацию
    }
}