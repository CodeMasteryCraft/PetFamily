using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public abstract class Photo : Entity
{
    public const string JPEG = ".jpeg";
    public const string JPG = ".jpg";
    public const string PNG = ".png";

    protected Photo(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; protected set; }
    public bool IsMain { get; protected set; }
}