using Microsoft.Extensions.DependencyInjection;
using PlayStationHub.Business.Authentication;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.Privileges;
using PlayStationHub.DTOs.User;
using System.Net;
using Utilities.Response;
using Utilities.Security;

namespace PlayStationHub.Business.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly JwtOptions _jwtOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IEnumerable<UserPrivilegeDto> _privileges;
    private UserDto _authenticatedUser;

    public AuthService(IServiceScopeFactory serviceScopeFactory, JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
        _serviceScopeFactory = serviceScopeFactory;

        using var scope = _serviceScopeFactory.CreateScope();
        _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
    }

    public IEnumerable<UserPrivilegeDto> Privileges => _privileges;
    public UserDto AuthenticatedUser => _authenticatedUser;
    public int? UserID { get; set; }
    public bool IsAuthenticated => UserID != null && _authenticatedUser != null;

    public bool IsAdmin => HasPrivilege(Enums.Privileges.Admin.ToString());
    public bool IsOwner => HasPrivilege(Enums.Privileges.Owner.ToString());
    public bool IsUser => HasPrivilege(Enums.Privileges.User.ToString());

    public async Task<ResponseOutcome<string>> LoginAsync(string username, string password)
    {
        var loginCredentials = await _userService.GetUserCredentialsByUsernameAsync(username);

        if (loginCredentials == null || !Hashing.CompareHashed(password, loginCredentials.Password))
        {
            return new ResponseOutcome<string>(null, HttpStatusCode.Unauthorized, "Invalid username or password");
        }

        var user = await _userService.FindAsync(loginCredentials.Username);
        await InitializeSessionAsync(user);

        var token = AuthenticationHelper.GenerateToken(_jwtOptions, user, _privileges.Select(privilege => privilege.Name).ToList());

        return new ResponseOutcome<string>(token, HttpStatusCode.OK, $"Welcome back, {loginCredentials.Username}");
    }

    public void Logout()
    {
        UserID = null;
        _authenticatedUser = null;
        _privileges = null;
    }

    public async Task RefreshAsync()
    {
        if (UserID.HasValue)
        {
            _authenticatedUser = await _userService.FindAsync(UserID.Value);
            _privileges = await _userService.GetUserPrivilege(UserID.Value);
        }
    }

    private bool HasPrivilege(string privilegeName)
    {
        return _privileges?.Any(privilege => privilege.Name == privilegeName) ?? false;
    }

    private async Task InitializeSessionAsync(UserDto user)
    {
        UserID = user.ID;
        _authenticatedUser = user;
        _privileges = await _userService.GetUserPrivilege((int)user.ID);
    }
}

