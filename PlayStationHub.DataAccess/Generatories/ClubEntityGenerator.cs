using Microsoft.Data.SqlClient;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Generatories;

public class ClubEntityGenerator
{
    public static Club GenerateClubEntity(SqlDataReader reader)
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
