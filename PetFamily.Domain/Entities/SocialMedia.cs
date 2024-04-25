using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

//TODO Result pattern + валидация
public class SocialMedia
{
    private SocialMedia(Guid id, string link, Social social)
    {
        Id = id;
        Link = link;
        Social = social;
    }

    public Guid Id { get; set; }
    public string Link { get; set; }
    public Social Social { get; set; }

    private static SocialMedia Create()
    {

    }
}