using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlayStationHub.API.Authentication;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Authentications;
using PlayStationHub.Business.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Utilities.Response;
using Utilities.Security;

namespace PlayStationHub.API.Controllers.Authentication;

[Route("api/auth/[controller]")]
[ApiController]
public class LoginController : BaseController<IUserService>
{
    private readonly JwtOptions _JWTOptions;
    public LoginController(JwtOptions JWTOptions, IUserService service, IMapper mapper) : base(service, mapper) { _JWTOptions = JWTOptions; }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<string>> Login(LoginRequest Request)
    {
        var user = await _Service.GetUserCredentialsByUsernameAsync(Request.Username);
        if (user == null || Hashing.CompareHashed(Request.Password, user.Password))
            return Unauthorized(new NullableResponseData(HttpStatusCode.Unauthorized, "Not found username or password in our credentials"));

        var TokenHandler = new JwtSecurityTokenHandler();
        var TokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _JWTOptions.Issuer,
            Audience = _JWTOptions.Audience,
            SigningCredentials = new SigningCredentials(BaseAuthenticationConfig.GetSymmetricSecurityKey(_JWTOptions.SigningKey), SecurityAlgorithms.HmacSha256),
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
        var Token = TokenHandler.WriteToken(SecurityToken);

        return Ok(Token);
    }
}
