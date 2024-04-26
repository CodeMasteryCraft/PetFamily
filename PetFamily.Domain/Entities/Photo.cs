namespace PetFamily.Domain.Entities;

public class Photo
{
    private Photo()
    {
    }

    public Photo(Guid id, string path, bool isMain)
    {
        Id = id;
        Path = path;
        IsMain = isMain;
    }

    public Guid Id { get; private set; }
    public string Path { get; private set; }
    public bool IsMain { get; private set; }
}