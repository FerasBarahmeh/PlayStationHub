using PlayStationHub.DTOs.Admins;
using PlayStationHub.DTOs.User;

namespace PlayStationHub.DTOs.Owners;

public class OwnerDto
{
    public int ID { get; set; }
    public AdminDto AddedBy { get; set; }
    public UserDto User { get; set; }
}
