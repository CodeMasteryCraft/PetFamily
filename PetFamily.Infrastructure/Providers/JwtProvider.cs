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
    private readonly IOptions<JwtOptions> _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public Result<string, Error> Generate(User user)
    {
        var jwtHandler = new JsonWebTokenHandler();

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey));

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
            Expires = DateTime.UtcNow.AddHours(_options.Value.Expires)
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);

        return token;
    }
}