using PlayStationHub.DTOs.Owners;

namespace PlayStationHub.DTOs.Clubs;

public class ClubDto
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public OwnerDto Owner { get; set; }
    public int DeviceCount { get; set; }
    public byte Status { get; set; }
    public string StatusName { get; set; }
}
