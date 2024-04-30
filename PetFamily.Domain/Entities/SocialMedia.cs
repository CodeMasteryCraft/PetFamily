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
    public string Link { get; private set; } = null!;
    public Social Social { get; private set; } = null!;

    public static Result<SocialMedia, Error> Create(string link, Social social)
    {
        link = link.Trim();

        if (link.IsEmpty() || link.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        return new SocialMedia(
            link,
            social);
    }
}