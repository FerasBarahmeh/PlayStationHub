using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.DataTransferObject.Users;

public class UserDTO
{
    public int? ID { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public byte Status { get; set; }
    public string StatusName
    {
        get
        {
            return Enum.GetName(typeof(UserStatus), Status);
        }
    }

}
