namespace PlayStationHub.Business.DataTransferObject.Privileges;

public class UserPrivilegeDTO
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int PrivilegeID { get; set; }
    public string Name { get; set; }
}
