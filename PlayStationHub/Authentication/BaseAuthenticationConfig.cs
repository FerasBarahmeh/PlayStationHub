using Microsoft.IdentityModel.Tokens;
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
}
