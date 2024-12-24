
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.DataTransferObject.Clubs;

public class ClubDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public Owner Owner { get; set; }
    public int DeviceCount { get; set; }
}
