using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;
using PetFamily.Infrastructure.Providers;

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

        var token = _jwtProvider.Create(request.Password, user.Value); 

        return token;
    }
}