using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;

namespace PetFamily.Application.Features.Users.Login;

public class LoginHandler
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginHandler(IUsersRepository usersRepository, IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginResponse, Error>> Handle(LoginRequest request, CancellationToken ct)
    {
        var user = await _usersRepository.GetByEmail(request.Email, ct);

        if (user.IsFailure)
            return user.Error;
        
        var isVerified = BCrypt.Net.BCrypt.Verify(request.Password, user.Value.PasswordHash);
        if (isVerified == false)
            return Errors.Users.InvalidCredentials();

        var token = _jwtProvider.Generate(user.Value);

        var response = new LoginResponse(token.Value, user.Value.Role.Name);
        return response;
    }
}