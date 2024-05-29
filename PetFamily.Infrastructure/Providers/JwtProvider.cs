using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PetFamily.Domain.Common;
using PetFamily.Domain.Entities;
using PetFamily.Infrastructure.Options;
using System.Security.Claims;
using System.Text;
using PetFamily.Application.Providers;

namespace PetFamily.Infrastructure.Providers;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public Result<string, Error> Generate(User user)
    {
        var jwtHandler = new JsonWebTokenHandler();

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var permissionClaims = user.Role.Permissions
            .Select(p => new Claim(Constants.Constants.Authentication.Permissions, p));

        var claims = permissionClaims.Concat(
        [
            new(Constants.Constants.Authentication.UserId, user.Id.ToString()),
            new(Constants.Constants.Authentication.Role, user.Role.Name)
        ]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(claims),
            SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256),
            Expires = DateTime.UtcNow.AddHours(_jwtOptions.Expires)
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);

        return token;
    }
}