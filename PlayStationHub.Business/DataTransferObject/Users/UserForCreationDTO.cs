using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.DataTransferObject.Users;

public class UserForCreationDTO
{
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public byte Status => (byte)UserStatus.Inactive;
}
