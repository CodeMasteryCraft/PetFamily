using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateSocialMedia;

public class CreateSocialMediaService
{
    
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateSocialMediaService(
        IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> Handle(CreateSocialMediaRequest request, CancellationToken ct)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, ct);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var social = Social.Create(request.Social).Value;
        var socialMedia = SocialMedia.Create(request.Link, social);
        
        if (socialMedia.IsFailure)
            return socialMedia.Error;

        volunteer.Value.PublishSocialMedia(socialMedia.Value);

        return await _volunteersRepository.Save(volunteer.Value, ct);
    }
}