namespace PlayStationHub.DataAccess.Entities;

public class PromptParams
{
    public int ID { get; set; }
    public string? Prompt { get; set; } = null;
    public DateTime? From { get; set; } = null!;
    public DateTime? To { get; set; } = null!;
}
