using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation;
using PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IUserRepository : IBaseBasicOperationRepository<User>, IPagedTableAsync<User>
{
    Task<User> FindAsync(string Username);
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<User> GetUserCredentialsByUsernameAsync(string Username);
    Task<IEnumerable<UserPrivilege>> GetUserPrivilege(int id);
}
