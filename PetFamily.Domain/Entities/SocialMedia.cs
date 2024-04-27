using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class SocialMedia
{
    private SocialMedia()
    {
    }

    public SocialMedia(string link, Social social)
    {
        Link = link;
        Social = social;
    }

    public Guid Id { get; private set; }
    public string Link { get; private set; }
    public Social Social { get; private set; }
    
    public static Result<SocialMedia, Error> Create(
        string link,
        Social social
        )
    {
      
        if (link.IsEmpty())
            return Errors.General.InvalidLength();
        
        return new SocialMedia(
            link,
            social
            );
    }
}