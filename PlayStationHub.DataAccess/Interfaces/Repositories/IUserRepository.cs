using PlayStationHub.DataAccess.Entities;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IUserRepository :
    IFindAsync<User>, IIsExistAsync, IIsExist, IDeleteAsync<int>, IInsertAsync<User>, IPagedTableAsync<User>, ICountRowsAsync, IUpdateAsync<User>
{
    Task<User> FindAsync(string Username);
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<User> GetUserCredentialsByUsernameAsync(string Username);
    Task<IEnumerable<UserPrivilege>> GetUserPrivilege(int id);
}
