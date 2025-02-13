
namespace PlayStationHub.Business.Requests.Clubs;

public class InsertFeedbackDto
{
    public string Feedback { get; set; }
    public int ClubID { get; set; }
    public byte Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
