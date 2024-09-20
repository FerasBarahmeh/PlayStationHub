using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IUserRepository : IBaseBasicOperationRepository<User>
{
    Task<User> FindAsync(string Username);
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<User> GetUserCredentialsByUsernameAsync(string Username);
    Task<IEnumerable<UserPrivilege>> GetUserPrivilege(int id);
}
