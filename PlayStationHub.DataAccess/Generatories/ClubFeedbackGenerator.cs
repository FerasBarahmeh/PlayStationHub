using Microsoft.Data.SqlClient;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Generators;

public class ClubFeedbackGenerator
{
    public static ClubFeedback Generate(SqlDataReader reader)
    {
        return new ClubFeedback
        {
            ID = reader.GetInt32(reader.GetOrdinal("ID")),
            Feedback = reader.GetString(reader.GetOrdinal("Feedback")),
            ClubID = reader.GetInt32(reader.GetOrdinal("ClubID")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            Status = reader.GetByte(reader.GetOrdinal("CreatedAt")),
            Club = reader.IsDBNull(reader.GetOrdinal("Name"))
                ? null
                : ClubEntityGenerator.Generate(reader),
        };
    }
}
