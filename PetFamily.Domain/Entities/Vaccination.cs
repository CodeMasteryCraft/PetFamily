using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class Vaccination : Entity
{
    private Vaccination()
    {
    }

    public Vaccination(string name, DateTimeOffset? applied)
    {
        Name = name;
        Applied = applied;
    }

    public string Name { get; private set; } = null!;

    public DateTimeOffset? Applied { get; private set; }

    public static Result<Vaccination, ResultEvent> Create(
        string name,
        DateTimeOffset? applied)
    {
        name = name.Trim();

        if (name.Length is < Constraints.MINIMUM_TITLE_LENGTH or > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(Vaccination));

        if (applied > DateTimeOffset.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(Vaccination));

        return new Vaccination(name, applied);
    }
}