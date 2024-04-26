using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

//TODO Result pattern + валидация
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

    public Guid Id { get; set; }
    public string Link { get; set; }
    public Social Social { get; set; }
}