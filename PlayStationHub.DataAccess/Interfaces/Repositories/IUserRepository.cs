using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> AllAsync();
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    bool IsExist(int ID);
    Task<bool> IsExistAsync(int ID);
    Task<User> FindAsync(int ID);
    Task<User> FindAsync(string Username);
    Task<int> InsertAsync(User UserInsertField);
    Task<int> DeleteAsync(int ID);
}
