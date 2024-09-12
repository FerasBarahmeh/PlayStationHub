namespace PlayStationHub.Business.DataTransferObject.Users.Requests;

public class UpdateUserRequest
{
    public int? ID { get; set; }
    public string? Username { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public byte? Status { get; set; }
}
