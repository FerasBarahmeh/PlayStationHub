using AutoMapper;
using PlayStationHub.Business.Requests.Clubs;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.Business.Profiles;

public class ClubFeedbackProfile : Profile
{
    public ClubFeedbackProfile()
    {
        CreateMap<ClubFeedback, ClubFeedbackDto>().ReverseMap();
        CreateMap<InsertFeedbackDto, ClubFeedbackDto>();
        CreateMap<GenerateSummaryForCommentsToClubDto, PromptParamsDto>();
        CreateMap<PromptParamsDto, PromptParams>();
    }
}
