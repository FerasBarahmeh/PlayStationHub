using Microsoft.IdentityModel.Tokens;
using PlayStationHub.Business.DataTransferObject.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PlayStationHub.API.Authentication;

public class BaseAuthenticationConfig
{
    public static byte[] GetAuthKey(string StringSigningKey)
    {
        return Encoding.UTF8.GetBytes(StringSigningKey);
    }
    public static SymmetricSecurityKey GetSymmetricSecurityKey(string StringSigningKey)
    {
        return new SymmetricSecurityKey(GetAuthKey(StringSigningKey));
    }
    public static string GenerateToken(JwtOptions jwtOptions, UserDTO user)
    {
        var TokenHandler = new JwtSecurityTokenHandler();
        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtOptions.Issuer,
            Audience = jwtOptions.Audience,
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(BaseAuthenticationConfig.GetSymmetricSecurityKey(jwtOptions.SigningKey), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new("ID", user.ID.ToString()),
                    new("Username", user.Username),
                    new("Email", user.Email),
                    new("Phone", user.Phone),
                    new("Status", user.Status.ToString()),
                    new("StatusName", user.StatusName.ToString()),
                }),
        };

        var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
        return TokenHandler.WriteToken(SecurityToken);
    }
}
