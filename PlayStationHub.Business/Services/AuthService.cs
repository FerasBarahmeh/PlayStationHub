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
    public readonly IUserService _userService;
    private readonly JwtOptions _JWTOptions;

    public AuthService(IUserService userService, JwtOptions jwtOptions)
    {
        _userService = userService;
        _JWTOptions = jwtOptions;
    }

    public async Task<ResponseOutcome<string>> LoginAsync(string username, string password)
    {
        var LoginCredentials = await _userService.GetUserCredentialsByUsernameAsync(username);
        if (LoginCredentials == null || !Hashing.CompareHashed(password, LoginCredentials.Password))
            return new ResponseOutcome<string>(null, HttpStatusCode.Unauthorized, "Not found username or password in our credentials");


        UserDTO user = await _userService.FindAsync(LoginCredentials.Username);
        IEnumerable<UserPrivilegeDTO> privileges = await _userService.GetUserPrivilege((int)user.ID);
        string Token = AuthenticationHelper.GenerateToken(_JWTOptions, user, privileges);

        return new ResponseOutcome<string>(Token, HttpStatusCode.OK, $"Welcome Back {LoginCredentials.Username}");
    }

    public async Task<UserDTO> User(int id) // TODO: get id by implement singleton for auth service
    {
        return await _userService.FindAsync(id);
    }

    public async Task<IEnumerable<UserPrivilegeDTO>> UserPrivileges(int id)
    {
        return await _userService.GetUserPrivilege(id);
    }
}
