using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using Utilities.Response;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IAuthService
{
    IEnumerable<UserPrivilegeDTO> Privileges { get; }
    UserDTO AuthenticatedUser { get; }
    public int? UserID { get; set; }
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    bool IsUser { get; }
    bool IsOwner { get; }
    Task<ResponseOutcome<string>> LoginAsync(string username, string password);
    void Logout();
    Task RefreshAsync();
}
