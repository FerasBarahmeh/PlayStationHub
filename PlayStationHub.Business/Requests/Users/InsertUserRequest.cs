namespace PlayStationHub.Business.Requests.Users;
public class InsertUserRequest
{
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}