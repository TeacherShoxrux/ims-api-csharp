using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using imsapi.Data;
using imsapi.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(int userId, string role, int storeId)
    {
         if (string.IsNullOrEmpty(role))
    {
        throw new ArgumentException("Role cannot be null or empty", nameof(role));
    }
        var claims = new[]
        {
            new Claim("userId", userId.ToString()),
            new Claim("userRole", role.ToString()),
            new Claim("storeId", storeId.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
