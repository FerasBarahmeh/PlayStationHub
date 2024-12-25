﻿using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.Business.Services;

public class ClubFeedbackService(IClubFeedbackRepository repo) : BaseService<IClubFeedbackRepository>(repo), IClubFeedbackService
{
    public async Task<string> GeneratePrompt(int ClubID)
    {
        return await _Repository.Prompt(ClubID);
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
}
