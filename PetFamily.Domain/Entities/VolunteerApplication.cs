using PetFamily.Domain.Common;
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
        Email email,
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
    public Email Email { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int YearsExperience { get; private set; }
    public int? NumberOfPetsFoundHome { get; private set; }
    public bool FromShelter { get; private set; }

    public Result Approve()
    {
        if (Status == ApplicationStatus.Approved)
            return Errors.VolunteersApplications.AlreadyApproved();
        
        Status = ApplicationStatus.Approved;
        return Result.Success();
    }
}

// одобрена
// на рассмотрении
// отклонена