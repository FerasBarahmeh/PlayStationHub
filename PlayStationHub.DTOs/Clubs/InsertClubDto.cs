namespace PlayStationHub.DTOs.Clubs;

public class InsertClubDto
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int OwnerID { get; set; }
    public int DeviceCount { get; set; }
    public byte Status { get; set; }
}
