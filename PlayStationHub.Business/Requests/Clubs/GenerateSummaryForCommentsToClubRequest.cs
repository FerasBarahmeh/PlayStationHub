using PlayStationHub.Business.Requests.Clubs.interfaces;

namespace PlayStationHub.Business.Requests.Clubs;

public class GenerateSummaryForCommentsToClubRequest : IID
{
    public int ID { get; set; }
    public string? Prompt { get; set; } = null;
    public DateTime? From { get; set; } = null;
    public DateTime? To { get; set; } = null;
}
