using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Repositories.BasicOperation.Async;
using PlayStationHub.Business.Interfaces.Repositories.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IUserService : IAllAsync<UserDTO>, IIsExistAsync, IFindAsync<UserDTO>, IDeleteAsync, IIsExist
{
    public ModeStatus Mode { get; }
    public string Password { get; set; }
    UserDTO UserModel { get; set; }
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<UserDTO> FindAsync(string Username);

    Task<bool> SaveAsync();

}
