using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.Entities;

namespace UserService.CQRS.AuthService;

public interface IJwtService
{
    string GenerateToken(UserEntity user);
}

public class JwtService(IConfiguration configuration) : IJwtService
{
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