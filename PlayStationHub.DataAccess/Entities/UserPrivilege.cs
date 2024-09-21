using Microsoft.Data.SqlClient;

namespace PlayStationHub.DataAccess.Entities;

public class UserPrivilege
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int PrivilegeID { get; set; }
    public string Name { get; set; }

    public static UserPrivilege GenerateOne(SqlDataReader reader)
    {
        return new UserPrivilege
        {
            ID = reader.GetInt32(reader.GetOrdinal("ID")),
            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
            PrivilegeID = reader.GetInt32(reader.GetOrdinal("PrivilegeID")),
            Name = reader.GetString(reader.GetOrdinal("Name"))
        };
    }
}
