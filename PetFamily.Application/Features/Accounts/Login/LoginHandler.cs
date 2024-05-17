using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Accounts.Login;

public class LoginHandler
{
    private readonly IUsersRepository _usersRepository;

    public LoginHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<string, Error>> Handle(LoginRequest request, CancellationToken ct)
    {
        var user = await _usersRepository.GetByEmail(request.Email, ct);

        if (user.IsFailure)
            return user.Error;

        var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Value.PasswordHash);

        if (isVerified == false)
            return Errors.Users.InvalidCredentials();

        // сгенерировать токен
        // вернуть токен
    }
}