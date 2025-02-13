using PlayStationHub.Business.Enums;
using PlayStationHub.DTOs.Privileges;
using PlayStationHub.DTOs.User;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;


public interface IUserService :
    IIsExistAsync, IFindAsync<UserDto>, IDeleteAsync<bool>, IIsExist, IPagedTableAsync<UserDto>, ICountRowsAsync
{
    public EnmMode Mode { get; }
    public string Password { get; set; }
    UserDto User { get; set; }
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<UserDto> FindAsync(string Username);
    Task<bool> SaveAsync();
    Task<UserLoginDto> GetUserCredentialsByUsernameAsync(string Username);
    Task<IEnumerable<UserPrivilegeDto>> GetUserPrivilege(int id);
}
