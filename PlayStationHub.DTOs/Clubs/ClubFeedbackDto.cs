
namespace PlayStationHub.DTOs.Clubs;
public class ClubFeedbackDto
{
    public int ID { get; set; }
    public string Feedback { get; set; }
    public int ClubID { get; set; }
    public ClubDto Club { get; set; }
    public DateTime CreatedAt { get; set; }
    public byte Status { get; set; }
}
