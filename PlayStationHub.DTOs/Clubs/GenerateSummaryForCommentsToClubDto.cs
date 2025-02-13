using PlayStationHub.DTOs.Clubs.interfaces;

namespace PlayStationHub.Business.Requests.Clubs;

public class GenerateSummaryForCommentsToClubDto : IID
{
    public int ID { get; set; }
    public string Prompt { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
