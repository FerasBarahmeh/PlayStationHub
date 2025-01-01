using PlayStationHub.Business.DataTransferObject.Clubs;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IGeminiService
{
    Task<string> GenerateResponseAsync(PromptParamsDTO ClubID);
}
