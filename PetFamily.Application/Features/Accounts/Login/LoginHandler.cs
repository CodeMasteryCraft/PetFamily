using System.Security.Claims;
using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
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

        var handler = new JsonWebTokenHandler();
        var key = "key18key18key18key18key18key18key18key18key18key18";

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var permissionClaims = user.Value.Role.Permissions
            .Select(p => new Claim(nameof(Permissions), p));

        var claims = permissionClaims.Concat(
        [
            new(Constants.Constants.UserId, user.Value.Id.ToString()),
            new(Constants.Constants.Role, user.Value.Role.Name)
        ]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(claims),
            SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}