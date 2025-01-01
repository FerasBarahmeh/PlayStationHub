namespace PlayStationHub.Business.DataTransferObject.Clubs;

public class PromptParamsDTO
{
    public int ID { get; set; }
    public string? Prompt { get; set; } = null;
    public DateTime? From { get; set; } = null!;
    public DateTime? To { get; set; } = null!;
}
