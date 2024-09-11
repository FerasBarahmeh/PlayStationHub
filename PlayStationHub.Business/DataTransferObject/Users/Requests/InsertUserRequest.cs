namespace PlayStationHub.Business.DataTransferObject.Users.Requests;

public class InsertUserRequest
{
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}