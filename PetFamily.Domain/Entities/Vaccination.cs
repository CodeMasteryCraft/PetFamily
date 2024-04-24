namespace PetFamily.Domain.Entities;

public class Vaccination
{
    private Vaccination()
    {
    }

    public Vaccination(string name, DateTime applied)
    {
        Name = name;
        Applied = applied;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public DateTimeOffset Applied { get; private set; }
}