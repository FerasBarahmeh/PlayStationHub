using Microsoft.Data.SqlClient;

namespace PlayStationHub.DataAccess.Entities;

public class Club
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int OwnerID { get; set; }
    public byte DeviceCount { get; set; }

    public static Club GenerateOne(SqlDataReader reader)
    {
        return new Club
        {
            ID = reader.GetInt32(reader.GetOrdinal("ID")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Location = reader.GetString(reader.GetOrdinal("Location")),
            OwnerID = reader.GetInt32(reader.GetOrdinal("OwnerID")),
            DeviceCount = reader.GetByte(reader.GetOrdinal("DeviceCount"))
        };
    }
}
