using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.DataTransferObject.Clubs.Requests;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.Mappers;

public class ClubFeedbackMapper
{
    public static ClubFeedback ToClubFeedbackEntity(ClubFeedbackDTO ClubFeedback)
    {
        return new ClubFeedback
        {
            ID = ClubFeedback.ID ?? null,
            Feedback = ClubFeedback.Feedback,
            ClubID = ClubFeedback.ClubID,
            Club   = ClubFeedback.Club,
            CreatedAt = ClubFeedback.CreatedAt,
            Status = ClubFeedback.Status,
        };
    }
    public static ClubFeedbackDTO ToClubFeedbackDTO(InsertFeedbackRequest ClubFeedback)
    {
        return new ClubFeedbackDTO
        {
            ID = null,
            Feedback = ClubFeedback.Feedback,
            ClubID = ClubFeedback.ClubID,
            CreatedAt = ClubFeedback.CreatedAt,
            Status = ClubFeedback.Status,
        };
    }
}
