using Microsoft.Extensions.Logging;
using PetFamily.Application.DataAccess;
using PetFamily.Application.Features.Users;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;

namespace PetFamily.Application.Features.VolunteerApplications.ApproveVolunteerApplication;

public class ApproveVolunteerApplicationHandler
{
    private readonly IVolunteerApplicationsRepository _volunteerApplicationsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ApproveVolunteerApplicationHandler> _logger;

    public ApproveVolunteerApplicationHandler(
        IVolunteerApplicationsRepository volunteerApplicationsRepository,
        IUsersRepository usersRepository,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<ApproveVolunteerApplicationHandler> logger)
    {
        _volunteerApplicationsRepository = volunteerApplicationsRepository;
        _usersRepository = usersRepository;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(ApproveVolunteerApplicationRequest request, CancellationToken ct)
    {
        var volunteerApplicationResult = await _volunteerApplicationsRepository.GetById(request.Id, ct);
        if (volunteerApplicationResult.IsFailure)
            return volunteerApplicationResult.Error;

        var volunteerApplication = volunteerApplicationResult.Value;

        var approvedResult = volunteerApplication.Approve();
        if (approvedResult.IsFailure)
            return approvedResult.Error;

        //TOO: рандомно сгенерировать пароль
        var user = User.CreateVolunteer(volunteerApplication.Email, "gsdflkjgldksjg");
        if (user.IsFailure)
            return user.Error;

        await _usersRepository.Add(user.Value, ct);

        var volunteer = Volunteer.Create(
            user.Value.Id,
            volunteerApplication.FullName,
            volunteerApplication.Description,
            volunteerApplication.YearsExperience,
            volunteerApplication.NumberOfPetsFoundHome,
            null,
            volunteerApplication.FromShelter,
            []);

        if (volunteer.IsFailure)
            return volunteer.Error;

        await _volunteersRepository.Add(volunteer.Value, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation(
            "Volunteer application has been successfully approved and volunteer has been created with id: {id}",
            volunteer.Value.Id);

        // отправить письмо на почту будущего волонтера
        // отправить уведомление в телеграмм

        return Result.Success();
    }
}