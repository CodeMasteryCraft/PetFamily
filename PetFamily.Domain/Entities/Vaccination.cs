using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Vaccination
{
    private Vaccination()
    {
    }

    public Vaccination(string name, DateTimeOffset applied)
    {
        Name = name;
        Applied = applied;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateTimeOffset Applied { get; private set; }

    public static Result<Vaccination, Error> Create(
        string name,
        DateTimeOffset applied)
    {
        if (name.IsEmpty() || name.Length > Constraints.SHORT_TITLE_LENGTH)
            return Errors.General.InvalidLength(nameof(Vaccination));

        if (applied.Year < Constraints.YEAR1900 || applied > DateTimeOffset.UtcNow)
            return Errors.General.InvalidLength(nameof(Vaccination));

        return new Vaccination(name, applied);
    }
}