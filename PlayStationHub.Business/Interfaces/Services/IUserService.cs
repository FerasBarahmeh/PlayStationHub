using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.BasicOperation.Async;
using PlayStationHub.Business.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;


public interface IUserService : IIsExistAsync, IFindAsync<UserDTO>, IDeleteAsync, IIsExist, PagedTableAsync<UserDTO>
{
    public ModeStatus Mode { get; }
    public string Password { get; set; }
    UserDTO UserModel { get; set; }
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<UserDTO> FindAsync(string Username);
    Task<bool> SaveAsync();
    Task<UserLoginDTO> GetUserCredentialsByUsernameAsync(string Username);
    Task<IEnumerable<UserPrivilegeDTO>> GetUserPrivilege(int id);
}
