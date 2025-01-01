using PlayStationHub.Business.DataTransferObject.Clubs.Requests.interfaces;

namespace PlayStationHub.Business.DataTransferObject.Clubs.Requests;

public class GenerateSummaryForCommentsToClubRequest : IID
{
    public int ID { get; set; }
    public string? Prompt { get; set; } = null;
    public DateTime? From { get; set; } = null;
    public DateTime? To { get; set; } = null;
}
