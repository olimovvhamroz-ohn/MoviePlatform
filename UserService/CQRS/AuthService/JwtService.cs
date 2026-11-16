using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.Entities;

namespace UserService.CQRS.AuthService;

public interface IJwtService
{
    string GenerateToken(UserEntity user);
    string GenerateRefreshToken();
   string HashRefreshToken(string refreshToken);

}

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rgn = RandomNumberGenerator.Create();
        rgn.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    public string HashRefreshToken(string refreshToken)
    {
        var bytes = SHA256.HashData(
            Encoding.UTF8.GetBytes(refreshToken));

        return Convert.ToBase64String(bytes);
    }

    public string GenerateToken(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var secretKey = configuration["Jwt:SecretKey"]
                        ?? throw new InvalidOperationException("JWT SecretKey is missing");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var minutes = configuration.GetValue<int?>("Jwt:ExpiresInMinutes") ?? 60;

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}