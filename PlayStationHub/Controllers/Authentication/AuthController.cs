using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Authentication;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Authentications;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Interfaces.Services;
using System.Net;
using Utilities.Response;
using Utilities.Security;

namespace PlayStationHub.API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController<IUserService>
{
    private readonly JwtOptions _JWTOptions;

    public AuthController(JwtOptions JWTOptions, IUserService service, IMapper mapper) : base(service, mapper)
    {
        _JWTOptions = JWTOptions;
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginRequest Request)
    {
        var LoginCredentials = await _Service.GetUserCredentialsByUsernameAsync(Request.Username);
        if (LoginCredentials == null || !Hashing.CompareHashed(Request.Password, LoginCredentials.Password))
            return Unauthorized(new NullableResponseData(HttpStatusCode.Unauthorized, "Not found username or password in our credentials"));

        string Token = BaseAuthenticationConfig.GenerateToken(_JWTOptions, LoginCredentials);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,    // Secure the cookie, making it inaccessible to JavaScript
            Secure = false,      // Send over HTTP and HTTPS
            SameSite = SameSiteMode.Unspecified, // Not prevent CSRF
            IsEssential = true,
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_JWTOptions.Lifetime)) // Expire the cookie after 30 Minute
        };

        Response.Cookies.Append("jwtToken", Token, cookieOptions);
        UserDTO user = await _Service.FindAsync(LoginCredentials.Username);
        return Ok(new ResponseOutcome<object>(new { user = LoginCredentials }, HttpStatusCode.OK, $"Welcome Back {LoginCredentials.Username}"));
    }

    [HttpGet("IsAuth")]
    [Authorize]
    public IActionResult CheckAuthentication()
    {
        return User.Identity.IsAuthenticated ? Ok(true) : Unauthorized(false);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwtToken");
        return Ok(new { message = "Logged out" });
    }

}
