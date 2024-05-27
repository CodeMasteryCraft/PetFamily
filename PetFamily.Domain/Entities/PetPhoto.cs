using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class PetPhoto : Photo
{
    public PetPhoto(string path, bool isMain) : base(path, isMain)
    {
    }
}