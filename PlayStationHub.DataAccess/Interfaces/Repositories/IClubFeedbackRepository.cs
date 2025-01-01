using PlayStationHub.DataAccess.Entities;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IClubFeedbackRepository: IInsertAsync<ClubFeedback>, IIsExistByContent
{
    Task<string> Prompt(PromptParams PromptParams);
    bool HasFeedback(int clubID);
    Task<IEnumerable<string>> GetFeedbacks(int ClubID);
}
