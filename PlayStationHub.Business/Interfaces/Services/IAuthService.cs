using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IAuthService
{
    Task<IEnumerable<UserPrivilegeDTO>> Privileges { get; }
    Task<UserDTO> AuthorizeUser { get; }
    void Constructing(int id);
    void Deconstructing();
}
