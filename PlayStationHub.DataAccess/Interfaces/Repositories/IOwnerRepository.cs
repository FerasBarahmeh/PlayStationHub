using PlayStationHub.DataAccess.Entities;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IOwnerRepository : IIsExist
{
    Task<IEnumerable<Owner>> GetOwnersCoreDetailsAsync();
}
