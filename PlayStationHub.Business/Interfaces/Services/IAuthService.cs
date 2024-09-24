using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using Utilities.Response;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IAuthService
{
    Task<IEnumerable<UserPrivilegeDTO>> Privileges { get; }
    UserDTO AuthUser { get; }
    public int? UserID { get; set; }
    Task<ResponseOutcome<string>> LoginAsync(string username, string password);

    void Logout();
    bool IsAuth { get; }
}
