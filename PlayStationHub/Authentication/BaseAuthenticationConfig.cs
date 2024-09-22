using Microsoft.IdentityModel.Tokens;
using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using System.IdentityModel.Tokens.Jwt;
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
    public static string GenerateToken(JwtOptions jwtOptions, UserDTO user, IEnumerable<UserPrivilegeDTO> privileges)
    {
        var TokenHandler = new JwtSecurityTokenHandler();

        var claims = new Dictionary<string, object>{
            {"ID", user.ID.ToString() },
            { "Username", user.Username },
            { "Email", user.Email },
            {"Phone", user.Phone },
            { "Status", user.Status.ToString() },
            { "StatusName", user.StatusName.ToString() },

        };

        claims.Add("privileges", privileges.Select(p => p.PrivilegeID.ToString()).ToList());
        claims.Add("privilegesNames", privileges.Select(p => p.Name).ToList());


        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtOptions.Issuer,
            Audience = jwtOptions.Audience,

            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(jwtOptions.SigningKey), SecurityAlgorithms.HmacSha256),
            Claims = claims,
        };

        var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
        return TokenHandler.WriteToken(SecurityToken);
    }
}
