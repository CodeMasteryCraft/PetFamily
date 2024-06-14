using PetFamily.Application.DataAccess;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Users.Register;

public class RegisterHandler
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterRequest request, CancellationToken ct)
    {
        var email = Email.Create(request.Email).Value;

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = User.CreateRegularUser(email, passwordHash);
        
        if (user.IsFailure)
            return user.Error;

        await _usersRepository.Add(user.Value, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}