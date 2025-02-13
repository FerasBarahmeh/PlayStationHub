using Microsoft.IdentityModel.Tokens;
using PlayStationHub.DTOs.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PlayStationHub.Business.Authentication;

public class AuthenticationHelper
{
    public static byte[] GetAuthKey(string StringSigningKey)
    {
        return Encoding.UTF8.GetBytes(StringSigningKey);
    }
    public static SymmetricSecurityKey GetSymmetricSecurityKey(string StringSigningKey)
    {
        return new SymmetricSecurityKey(GetAuthKey(StringSigningKey));
    }
    public static string GenerateToken(JwtOptions jwtOptions, UserDto user, List<string> privileges)
    {
        var claims = new List<Claim>
        {
            new("ID", user.ID.ToString()),
            new("Username", user.Username),
            new("Email", user.Email),
            new("Phone", user.Phone),
            new("Status", user.Status.ToString()),
            new("StatusName", user.StatusName.ToString()),
        }
            .Concat(privileges.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToList();

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(jwtOptions.SigningKey), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
