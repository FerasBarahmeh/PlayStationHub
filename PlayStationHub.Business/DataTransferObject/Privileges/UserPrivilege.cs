namespace PlayStationHub.Business.DataTransferObject.Privileges;

public class UserPrivilege
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int PrivilegeID { get; set; }
    public string Name { get; set; }
}
