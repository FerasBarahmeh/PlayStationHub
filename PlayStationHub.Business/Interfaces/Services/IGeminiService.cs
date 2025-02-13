using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IGeminiService
{
    Task<string> GenerateResponseAsync(PromptParamsDto ClubID);
}
