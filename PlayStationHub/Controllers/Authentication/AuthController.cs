using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.Authentication;
using PlayStationHub.Business.DataTransferObject.Authentications;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Interfaces.Services;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController(JwtOptions _JWTOptions, IAuthService service) : BaseController<IAuthService>(service)
{
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest Request)
    {
        ResponseOutcome<string> Token = await _Service.LoginAsync(Request.Username, Request.Password);
        int? UserId = _Service.UserID;
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

        Response.Cookies.Append("jwtToken", Token.Response.ToString(), cookieOptions);

        return Ok(new ResponseOutcome<object>(true, Token.StatusCode, Token.Message));
    }

    [HttpPost("IsAuth")]
    public IActionResult IsAuth()
    {
        return _Service.IsAuthenticated ? Ok(true) : Unauthorized(false);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwtToken");
        _Service.Logout();
        return Ok(new { message = "Logged out" });
    }

    [HttpGet("AuthorizedUser")]
    public IActionResult AuthorizedUser()
    {
        UserDTO user = _Service.AuthenticatedUser;
        return user != null ?
            Ok(new ResponseOutcome<UserDTO>(user, HttpStatusCode.OK, ""))
            : StatusCode((int)HttpStatusCode.NotFound, "Not Found the user");
    }

    [HttpGet("IsAdmin")]
    public IActionResult IsAdmin()
    {
        return Ok(new ResponseOutcome<bool>(_Service.IsAdmin, HttpStatusCode.OK, ""));
    }

    [HttpGet("IsOwner")]
    public IActionResult IsOwner()
    {
        return Ok(new ResponseOutcome<bool>(_Service.IsOwner, HttpStatusCode.OK, ""));
    }

    [HttpGet("IsUser")]
    public IActionResult IsUser()
    {
        return Ok(new ResponseOutcome<bool>(_Service.IsUser, HttpStatusCode.OK, ""));
    }
}
