using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using System.Data;

namespace PlayStationHub.DataAccess.Repositories;

public class ClubFeedbackRepository(IConfiguration configuration) : BaseRepository<Club>(configuration), IClubFeedbackRepository
{
    public async Task<int> InsertAsync(ClubFeedback InsertField)
    {
        string Query = @"INSERT INTO ClubFeedbacks
                         (Feedback, ClubID, CreatedAt, Status)
                         VALUES (@Feedback, @ClubID, @CreatedAt, @Status);
                        SELECT SCOPE_IDENTITY() AS ID;";

        return await PredicateExecuteScalarAsync<int>(Query, async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Feedback", InsertField.Feedback);
            cmd.Parameters.AddWithValue("@ClubID", InsertField.ClubID);
            cmd.Parameters.AddWithValue("@CreatedAt", InsertField.CreatedAt);
            cmd.Parameters.AddWithValue("@Status", InsertField.Status);
            await Task.CompletedTask;
        });
    }

    public bool IsExist(string content)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM ClubFeedbacks WhERE Feedback=@Feedback;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Feedback", content);
            await Task.CompletedTask;
        });
    }
}
