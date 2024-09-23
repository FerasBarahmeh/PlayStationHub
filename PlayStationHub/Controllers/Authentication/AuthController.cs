using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Authentication;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.Authentication;
using PlayStationHub.Business.DataTransferObject.Authentications;
using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Interfaces.Services;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : BaseController<IAuthService>
{
    private readonly JwtOptions _JWTOptions;
    private readonly ClaimsHelper _claimsHelper;
    public AuthController(JwtOptions JWTOptions, IAuthService service, ClaimsHelper claimsHelper) : base(service)
    {
        _JWTOptions = JWTOptions;
        _claimsHelper = claimsHelper;

    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest Request)
    {
        ResponseOutcome<string> Token = await _Service.LoginAsync(Request.Username, Request.Password);

        if (Token.StatusCode == HttpStatusCode.Unauthorized)
            return Unauthorized(new NullableResponseData(Token.StatusCode, Token.Message));

        CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,    // Secure the cookie, making it inaccessible to JavaScript
            Secure = false,      // Send over HTTP and HTTPS
            SameSite = SameSiteMode.Unspecified, // Not prevent CSRF
            IsEssential = true,
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_JWTOptions.Lifetime)),
        };

        Response.Cookies.Append("jwtToken", Token.Data.ToString(), cookieOptions);

        return Ok(new ResponseOutcome<object>(true, Token.StatusCode, Token.Message));
    }

    [HttpGet("IsAuth")]
    public IActionResult CheckAuthentication()
    {
        return User.Identity.IsAuthenticated ? Ok(true) : Unauthorized(false);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwtToken");

        return Ok(new { message = "Logged out" });
    }

    [HttpPost("AuthorizedUser")]
    public async Task<IActionResult> AuthorizedUser()
    {
        UserDTO user = await _Service.User((int)_claimsHelper.ID);
        return user != null ?
            Ok(new ResponseOutcome<UserDTO>(user, HttpStatusCode.OK, ""))
            : StatusCode((int)HttpStatusCode.NotFound, "Not Found the user");
    }

    [HttpPost("UserPrivileges")]
    public async Task<IActionResult> UserPrivileges()
    {
        IEnumerable<UserPrivilegeDTO> privileges = await _Service.UserPrivileges((int)_claimsHelper.ID);
        return Ok(new ResponseOutcome<IEnumerable<UserPrivilegeDTO>>(privileges, HttpStatusCode.OK, "Authorized user privileges"));
    }
    [HttpPost("HasPrivilege")]
    public async Task<IActionResult> HasPrivileges([FromBody] int PrivilegeID)
    {
        IEnumerable<UserPrivilegeDTO> privileges = await _Service.UserPrivileges((int)_claimsHelper.ID);
        return Ok(new ResponseOutcome<bool>(
            privileges.FirstOrDefault(privilege => privilege.PrivilegeID == PrivilegeID)?.ID != null,
            HttpStatusCode.OK,
            "Authorized user privileges"));
    }
}
