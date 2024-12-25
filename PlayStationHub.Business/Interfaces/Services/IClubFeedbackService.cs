using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IClubFeedbackService : IInsertAsync<ClubFeedbackDTO>, IIsExistByContent
{
     Task<string> GeneratePrompt(int ClubID);
}
