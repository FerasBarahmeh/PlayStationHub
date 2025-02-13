using Microsoft.Data.SqlClient;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Generatories;

public class ClubEntityGenerator
{
    public static Club Generate(SqlDataReader reader)
    {
        return new Club
        {
            ID = reader.GetInt32(reader.GetOrdinal("ClubID")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Location = reader.GetString(reader.GetOrdinal("Location")),
            Owner = new Owner
            {
                ID = reader.GetInt32(reader.GetOrdinal("OwnerID")),
                AddedBy = AdminEntityGenerator.Generate(reader, "Admin", "Admin"),
                User = UserEntityGenerator.Generate(reader, "Owner"),
            },
            DeviceCount = reader.GetByte(reader.GetOrdinal("DeviceCount")),
            Status = reader.GetByte(reader.GetOrdinal("ClubStatus"))
        };
    }
}
