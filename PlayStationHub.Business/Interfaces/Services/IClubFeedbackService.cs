using PlayStationHub.DTOs.Clubs;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IClubFeedbackService : IInsertAsync<ClubFeedbackDto>, IIsExistByContent
{
    Task<string> GeneratePrompt(PromptParamsDto PromptParams);
    bool HasFeedback(int clubID);
    Task<IEnumerable<string>> GetFeedbacks(int ClubID);
}
