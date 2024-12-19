namespace PlayStationHub.DataAccess.Entities;

public class Owner
{
    public int ID { get; set; }
    public Admin AddedBy { get; set; }
    public User User { get; set; }
}
