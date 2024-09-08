using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> AllAsync();
}
