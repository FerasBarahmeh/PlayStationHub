using System.Security.Claims;

namespace PlayStationHub.API.Authentication;

public class ClaimsHelper
{
    private readonly IHttpContextAccessor _HttpContextAccessor;
    public ClaimsPrincipal User => _HttpContextAccessor.HttpContext?.User;
    public int? ID
    {
        get
        {

            if (int.TryParse(User.FindFirst("ID")?.Value, out int userId))
                return userId;

            return null;
        }
    }

    public string Username
    {
        get
        {
            return _HttpContextAccessor.HttpContext?.User.FindFirstValue("Username");
        }
    }

    public ClaimsHelper(IHttpContextAccessor HttpContextAccessor)
    {
        _HttpContextAccessor = HttpContextAccessor;
    }
}
