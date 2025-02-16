using Microsoft.Data.SqlClient;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Generators
{
    class CoreOwnerGenerator
    {
        public static Owner GenerateCoreOwnerDetails(SqlDataReader reader)
        {
            return new Owner
            {
                ID = reader.GetInt32(reader.GetOrdinal("OwnerID")),
                User = new User
                {
                    ID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    Username = reader.GetString(reader.GetOrdinal("OwnerUsername")),
                },
            };
        }
    }
}
