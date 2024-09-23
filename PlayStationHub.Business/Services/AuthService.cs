using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Interfaces.Services;

namespace PlayStationHub.Business.Services;

public class AuthService : IAuthService
{
    public readonly IUserService _userService;

    private IEnumerable<UserPrivilegeDTO> _privileges;

    private int _userID;
    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public Task<IEnumerable<UserPrivilegeDTO>> Privileges
    {
        get
        {
            if (_privileges == null && int.IsPositive(_userID))
                return _userService.GetUserPrivilege(_userID);
            return null;
        }
    }

    public Task<UserDTO> AuthorizeUser
    {
        get
        {
            if (int.IsNegative(_userID)) return null;
            return _userService.FindAsync(_userID);
        }
    }


    public void Constructing(int id)
    {
        _userID = id;
    }
    public void Deconstructing()
    {
        _userID = -1;
    }
}
