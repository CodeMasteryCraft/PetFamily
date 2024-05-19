using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Application.Constants;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.Options;
using System.Security.Claims;
using System.Text;

namespace PetFamily.Infrastructure.Providers;

public class JwtProvider : IJwtProvider
{
    private readonly IOptions<JwtOptions> _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public Result<string, Error> Create(string password, User user)
    {
        var isVerified = BCrypt.Net.BCrypt.EnhancedVerify(password, user.PasswordHash);
        if (isVerified == false)
            return Errors.Users.InvalidCredentials();

        var handler = new JsonWebTokenHandler();

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey));

        var permissionClaims = user.Role.Permissions
            .Select(p => new Claim(nameof(Permissions), p));

        var claims = permissionClaims.Concat(
        [
            new(Constants.UserId, user.Id.ToString()),
            new(Constants.Role, user.Role.Name)
        ]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(claims),
            SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256),
            Expires = DateTime.UtcNow.AddHours(_options.Value.Expires)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
