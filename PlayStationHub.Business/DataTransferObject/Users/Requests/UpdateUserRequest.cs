using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.DataTransferObject.Users.Requests;

public class UpdateUserRequest
{
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public UserStatus Status { get; set; }
}
