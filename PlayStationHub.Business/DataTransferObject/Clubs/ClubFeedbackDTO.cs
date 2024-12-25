using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.DataTransferObject.Clubs;

public class ClubFeedbackDTO
{
    public int? ID { get; set; } = null;
    public string Feedback { get; set; }
    public int ClubID { get; set; }
    public Club? Club { get; set; } = null;
    public DateTime CreatedAt { get; set; }
    public byte Status { get; set; } = 1;
}
