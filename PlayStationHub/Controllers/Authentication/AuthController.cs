using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Authentication;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Authentications;
using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Interfaces.Services;
using System.Net;
using Utilities.Response;
using Utilities.Security;

namespace PlayStationHub.API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : BaseController<IUserService>
{
    private readonly JwtOptions _JWTOptions;
    private readonly ClaimsHelper _claimsHelper;
    public AuthController(JwtOptions JWTOptions, IUserService service, ClaimsHelper claimsHelper) : base(service)
    {
        _JWTOptions = JWTOptions;
        _claimsHelper = claimsHelper;
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginRequest Request)
    {
        var LoginCredentials = await _Service.GetUserCredentialsByUsernameAsync(Request.Username);
        if (LoginCredentials == null || !Hashing.CompareHashed(Request.Password, LoginCredentials.Password))
            return Unauthorized(new NullableResponseData(HttpStatusCode.Unauthorized, "Not found username or password in our credentials"));


        UserDTO user = await _Service.FindAsync(LoginCredentials.Username);
        IEnumerable<UserPrivilegeDTO> privileges = await _Service.GetUserPrivilege((int)user.ID);
        string Token = BaseAuthenticationConfig.GenerateToken(_JWTOptions, user, privileges);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,    // Secure the cookie, making it inaccessible to JavaScript
            Secure = false,      // Send over HTTP and HTTPS
            SameSite = SameSiteMode.Unspecified, // Not prevent CSRF
            IsEssential = true,
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_JWTOptions.Lifetime)) // Expire the cookie after 30 Minute
        };

        Response.Cookies.Append("jwtToken", Token, cookieOptions);

        return Ok(new ResponseOutcome<object>(user, HttpStatusCode.OK, $"Welcome Back {LoginCredentials.Username}"));
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
        UserDTO user = await _Service.FindAsync((int)_claimsHelper.ID);
        return user != null ?
            Ok(new ResponseOutcome<UserDTO>(user, HttpStatusCode.OK, ""))
            : StatusCode((int)HttpStatusCode.NotFound, "Not Found the user");
    }

    [HttpPost("UserPrivileges")]
    public async Task<IActionResult> UserPrivileges()
    {
        IEnumerable<UserPrivilegeDTO> privileges = await _Service.GetUserPrivilege((int)_claimsHelper.ID);
        return Ok(new ResponseOutcome<IEnumerable<UserPrivilegeDTO>>(privileges, HttpStatusCode.OK, "Authorized user privileges"));
    }
}
