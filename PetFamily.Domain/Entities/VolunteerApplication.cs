using PetFamily.Domain.ValueObjects;
using Entity = PetFamily.Domain.Common.Entity;

namespace PetFamily.Domain.Entities;

public class VolunteerApplication : Entity
{
    private VolunteerApplication()
    {
    }

    public VolunteerApplication(
        FullName fullName,
        string email,
        string description,
        int yearsExperience,
        int? numberOfPetsFoundHome,
        bool fromShelter)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsExperience = yearsExperience;
        NumberOfPetsFoundHome = numberOfPetsFoundHome;
        FromShelter = fromShelter;
        Status = ApplicationStatus.Consideration;
    }

    public ApplicationStatus Status { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int YearsExperience { get; private set; }
    public int? NumberOfPetsFoundHome { get; private set; }
    public bool FromShelter { get; private set; }

    public void Approve()
    {
        Status = ApplicationStatus.Approved;
    }
}

// одобрена
// на рассмотрении
// отклонена