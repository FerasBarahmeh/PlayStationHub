namespace PlayStationHub.Business.Interfaces.Services;

public interface IGeminiService
{
    Task<string> GenerateResponseAsync(int ClubID);
}
