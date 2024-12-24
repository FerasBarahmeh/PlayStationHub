using Microsoft.Data.SqlClient;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Generatories;

public class AdminEntityGenerator
{

    public static Admin Generate(SqlDataReader reader)
    {
        return new Admin()
        {
            ID = reader.GetInt32(reader.GetOrdinal("AdminID")),
            User = UserEntityGenerator.Generate(reader),
        };
    }
    public static Admin Generate(SqlDataReader reader, string ColumnPrefixForAdmin, string ColumnPrefixForUser)
    {
        return new Admin()
        {
            ID = reader.GetInt32(reader.GetOrdinal(ColumnPrefixForAdmin+"ID")),
            User = UserEntityGenerator.Generate(reader, ColumnPrefixForUser),
        };
    }
}
