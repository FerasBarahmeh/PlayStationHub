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
    private IEnumerable<UserPrivilegeDTO> _privileges;
    private UserDTO _user;

    public IEnumerable<UserPrivilegeDTO> Privileges
    {
        get
        {
            if (UserID == null || _user == null) return null;
            if (UserID != null && _privileges == null) _SetUserPrivileges();

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

    public bool IsAdmin => _HasPrivilege(nameof(Enums.Privileges.Admin));
    public bool IsOwner => _HasPrivilege(nameof(Enums.Privileges.Owner));
    public bool IsUser => _HasPrivilege(nameof(Enums.Privileges.User));

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
        Init(user);
        string Token = AuthenticationHelper.GenerateToken(_JWTOptions, user, Privileges);

        return new ResponseOutcome<string>(Token, HttpStatusCode.OK, $"Welcome Back {LoginCredentials.Username}");
    }
    public void Logout()
    {
        UserID = null;
        _user = null;
    }
    public async void Refresh()
    {
        _user = await _userService.FindAsync((int)UserID);
        _privileges = null;
    }
    private async void _SetUserPrivileges()
    {
        _privileges = await _userService.GetUserPrivilege((int)UserID);
    }
    private bool _HasPrivilege(string privilegeName)
    {
        return !string.IsNullOrEmpty(Privileges?.FirstOrDefault(privilege => privilege.Name == privilegeName)?.Name);
    }
    private async void Init(UserDTO user)
    {
        UserID = (int)user.ID;
        _user = user;
        _privileges = await _userService.GetUserPrivilege((int)UserID);
    }
}
