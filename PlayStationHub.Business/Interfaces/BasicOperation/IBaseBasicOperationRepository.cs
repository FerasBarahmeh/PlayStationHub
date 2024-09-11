using PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;
using PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Repositories.BasicOperation;

public interface IBaseBasicOperationRepository<T> : IFindAsync<T>, IIsExistAsync, IIsExist, IDeleteAsync, IAllAsync<T>, IInsertAsync<T>
{

}
