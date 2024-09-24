using Microsoft.Extensions.DependencyInjection;
using PlayStationHub.Business.Authentication;
using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Interfaces.Services;
using System.Net;
using Utilities.Response;
using Utilities.Security;

namespace PlayStationHub.Business.Services;

public class AuthService : IAuthService
{
    private IUserService _userService;
    private readonly JwtOptions _JWTOptions;
    private IServiceScopeFactory _serviceScopeFactory;
    private Task<IEnumerable<UserPrivilegeDTO>> _privileges;
    private UserDTO _user;

    public Task<IEnumerable<UserPrivilegeDTO>> Privileges
    {
        get
        {
            if (UserID == null || _user == null) return null;
            if (_privileges != null && UserID != null) return _privileges;

            _privileges = _userService.GetUserPrivilege((int)UserID);
            return _privileges;
        }
    }

    public UserDTO AuthUser
    {
        get
        {
            return _user;
        }
    }
    public bool IsAuth
    {
        get
        {
            return UserID != null && _user != null;
        }
    }
    public int? UserID { get; set; }

    public AuthService(IServiceScopeFactory serviceScope, JwtOptions jwtOptions)
    {
        _JWTOptions = jwtOptions;
        _serviceScopeFactory = serviceScope;
        using var scope = _serviceScopeFactory.CreateScope();
        _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
    }

    public async Task<ResponseOutcome<string>> LoginAsync(string username, string password)
    {
        var LoginCredentials = await _userService.GetUserCredentialsByUsernameAsync(username);
        if (LoginCredentials == null || !Hashing.CompareHashed(password, LoginCredentials.Password))
            return new ResponseOutcome<string>(null, HttpStatusCode.Unauthorized, "Not found username or password in our credentials");


        UserDTO user = await _userService.FindAsync(LoginCredentials.Username);
        IEnumerable<UserPrivilegeDTO> privileges = await _userService.GetUserPrivilege((int)user.ID);
        string Token = AuthenticationHelper.GenerateToken(_JWTOptions, user, privileges);
        UserID = (int)user.ID;
        _user = user;
        return new ResponseOutcome<string>(Token, HttpStatusCode.OK, $"Welcome Back {LoginCredentials.Username}");
    }
    public void Logout()
    {
        UserID = null;
        _user = null;
    }


}
