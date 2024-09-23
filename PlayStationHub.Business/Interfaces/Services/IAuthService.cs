using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using Utilities.Response;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IAuthService
{
    Task<ResponseOutcome<string>> LoginAsync(string username, string password);
    Task<IEnumerable<UserPrivilegeDTO>> Privileges { get; }
    Task<UserDTO> AuthorizeUser { get; }
    void Constructing(int id);
    void Deconstructing();
    Task<UserDTO> User(int id);
    Task<IEnumerable<UserPrivilegeDTO>> UserPrivileges(int id);
}
