
namespace PlayStationHub.Business.DataTransferObject.Clubs;

public class ClubDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int OwnerID { get; set; }
    public int DeviceCount { get; set; }
}
