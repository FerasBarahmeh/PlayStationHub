using PlayStationHub.DataAccess.Entities;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IClubFeedbackRepository: IInsertAsync<ClubFeedback>, IIsExistByContent
{

}
