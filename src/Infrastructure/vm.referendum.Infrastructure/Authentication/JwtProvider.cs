using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using vm.referendum.Domain.Entities;
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
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(JwtRegisteredClaimNames.Name, user.FullName)
        };

        var permissions = await permissionService.GetPermissionsAsync(user.Id);

        foreach (var permission in permissions) claims.Add(new Claim(CustomClaims.PERMISSIONS, permission));

        var tokenExpirationTime = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}