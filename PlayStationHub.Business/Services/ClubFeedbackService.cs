using Microsoft.Identity.Client;
using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.Business.Services;

public class ClubFeedbackService(IClubFeedbackRepository repo) : BaseService<IClubFeedbackRepository>(repo), IClubFeedbackService
{
    public async Task<string> GeneratePrompt(PromptParamsDTO PromptParams)
    {
        if (PromptParams.Prompt == "")
            PromptParams.Prompt = "You are a helpful assistant. Summarize the following feedback comments into a cohesive summary that captures the key themes, strengths, and areas for improvement. Feedback: ";
        if (PromptParams.From == null)
            PromptParams.From = DateTime.Now - TimeSpan.FromDays(7);
        if (PromptParams.To == null)
            PromptParams.To = DateTime.Now;

        return await _Repository.Prompt(ClubFeedbackMapper.ToPromptParams(PromptParams));
    }

    public async Task<int> InsertAsync(ClubFeedbackDTO InsertField)
    {
        return await _Repository.InsertAsync(ClubFeedbackMapper.ToClubFeedbackEntity(InsertField));
    }

    public bool IsExist(string content)
    {
        return _Repository.IsExist(content);
    }

    public bool HasFeedback(int clubID)
    {
        return _Repository.HasFeedback(clubID);
    }

    public async Task<IEnumerable<string>> GetFeedbacks(int ClubID)
    {
        return await _Repository.GetFeedbacks(ClubID);
    }
}
