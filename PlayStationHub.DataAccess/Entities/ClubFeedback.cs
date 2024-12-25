namespace PlayStationHub.DataAccess.Entities;

public class ClubFeedback
{
    public int? ID { get; set; }
    public string Feedback { get; set; }
    public int ClubID { get; set; }
    public Club? Club { get; set; } = null;
    public DateTime CreatedAt { get; set; }
    public byte Status { get; set; } = 1;
}
