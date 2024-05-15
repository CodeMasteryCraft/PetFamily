using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;
using ValueObject = PetFamily.Domain.Common.ValueObject;

namespace PetFamily.Domain.Entities;

public class SocialMedia : ValueObject
{
    private SocialMedia(string link, Social social)
    {
        Link = link;
        Social = social;
    }

    public string Link { get; private set; }
    public Social Social { get; private set; }

    public static Result<SocialMedia, Error> Create(string link, Social social)
    {
        link = link.Trim();

        if (link.IsEmpty() || link.Length > Constraints.LONG_TITLE_LENGTH)
            return Errors.General.InvalidLength();

        return new SocialMedia(
            link,
            social);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Link;
        yield return Social;
    }
}