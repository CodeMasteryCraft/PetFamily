namespace PetFamily.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; } = Guid.Empty;

    public static bool operator == (Entity? first, Entity? second)
    {
        if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
            return true;

        if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            return false;

        return first.Equals(second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other) == false)
            return false;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}