using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using vm.referendum.Domain.Entities;
using vm.referendum.Domain.Entities.User;
using vm.referendum.Infrastructure.Authentication.Authentication;
using vm.referendum.Infrastructure.Authentication.Settings;

namespace vm.referendum.Infrastructure.Authentication;

/// <summary>
///     Represents the JWT provider.
/// </summary>
public sealed class JwtProvider(IOptions<JwtSettings> jwtOptions, IPermissionService permissionService)
    : IJwtProvider
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    /// <inheritdoc />
    public async Task<string> GenerateAsync(User user)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
            new Claim(JwtRegisteredClaimNames.Name, user.FullName)
        };

        HashSet<string> permissions = await permissionService.GetPermissionsAsync(user.Id);

        foreach (string permission in permissions)
        {
            claims.Add(new Claim(CustomClaims.PERMISSIONS, permission));
        }

        DateTime tokenExpirationTime = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        JwtSecurityToken token = new(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        string? tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}