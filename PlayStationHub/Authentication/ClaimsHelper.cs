using System.Security.Claims;

namespace PlayStationHub.API.Authentication;

public static class ClaimsHelper
{
    public static int? ID(ClaimsPrincipal user)
    {

        var userIdClaim = user.FindFirst("ID")?.Value;

        if (int.TryParse(userIdClaim, out int userId))
            return userId;

        return null;
    }
    public static string Username(ClaimsPrincipal user)
    {
        return user.FindFirst("Username")?.Value;
    }

}
