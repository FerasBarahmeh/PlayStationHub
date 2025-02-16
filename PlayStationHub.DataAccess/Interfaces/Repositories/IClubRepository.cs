using PlayStationHub.DataAccess.Entities;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;
using Utilities.Interfaces.BasicOperation.Async;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IClubRepository : IAllAsync<Club>, IIsExist, IIsExistByContent, IFindAsync<Club>, IInsertAsync<Club>, ISoftDeleteAsync
{

}
