using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Accounts.Login;

public class LoginHandler
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginHandler(IUsersRepository usersRepository, IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string, Error>> Handle(LoginRequest request, CancellationToken ct)
    {
        var user = await _usersRepository.GetByEmail(request.Email, ct);

        if (user.IsFailure)
            return user.Error;
        
        var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Value.PasswordHash);
        if (isVerified == false)
            return Errors.Users.InvalidCredentials();

        var token = _jwtProvider.Generate(user.Value); 

        return token;
    }
}