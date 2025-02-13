using AutoMapper;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.Business.Services;

public class ClubFeedbackService(IClubFeedbackRepository repo, IMapper _Mapper) : BaseService<IClubFeedbackRepository>(repo), IClubFeedbackService
{
    public async Task<string> GeneratePrompt(PromptParamsDto PromptParams)
    {
        if (PromptParams.Prompt == "")
            PromptParams.Prompt = "You are a helpful assistant. Summarize the following feedback comments into a cohesive summary that captures the key themes, strengths, and areas for improvement. Feedback: ";
        if (PromptParams.From == null)
            PromptParams.From = DateTime.Now - TimeSpan.FromDays(7);
        if (PromptParams.To == null)
            PromptParams.To = DateTime.Now;

        return await _Repository.Prompt(_Mapper.Map<PromptParams>(PromptParams));
    }

    public async Task<int> InsertAsync(ClubFeedbackDto InsertField)
    {
        return await _Repository.InsertAsync(_Mapper.Map<ClubFeedback>(InsertField));
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
