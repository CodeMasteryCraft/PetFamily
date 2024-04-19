using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.Entities;

public class Vaccination
{
    private Vaccination(Guid id,string name, DateTimeOffset applied)
    {
        Id = id;
        Name = name;
        Applied = applied;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateTimeOffset Applied { get; private set; }

    public static Result<Vaccination, Error> Create(Guid id,string name, DateTimeOffset applied)
    {
        if(name.IsEmpty())
            Errors.General.ValueIsInvalid(name);

        if (applied > DateTimeOffset.Now)
            Errors.General.InvalidIsDate();

        return new Vaccination(id, name,applied);
    }
}