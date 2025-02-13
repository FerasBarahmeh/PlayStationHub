using PlayStationHub.DTOs.Privileges;
using PlayStationHub.DTOs.User;
using Utilities.Response;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IAuthService
{
    IEnumerable<UserPrivilegeDto> Privileges { get; }
    UserDto AuthenticatedUser { get; }
    public int? UserID { get; set; }
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    bool IsUser { get; }
    bool IsOwner { get; }
    Task<ResponseOutcome<string>> LoginAsync(string username, string password);
    void Logout();
    Task RefreshAsync();
}
