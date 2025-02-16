using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Generators;
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

    public async Task<string> Prompt(PromptParams PromptParams)
    {
        string Query = @"
                SELECT CONCAT(
                '@Prompt',
                STRING_AGG(Feedback, '; ')
                ) AS Prompt
                FROM ClubFeedbacks
                WHERE (ClubID = @ClubID) AND (ClubFeedbacks.CreatedAt BETWEEN CAST(@From AS DATETIME) AND CAST(@To AS DATETIME));
        ";
        return await PredicateExecuteScalarAsync<string>(Query, async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Prompt", PromptParams.Prompt);
            cmd.Parameters.AddWithValue("@ClubID", PromptParams.ID);
            cmd.Parameters.AddWithValue("@From", PromptParams.From);
            cmd.Parameters.AddWithValue("@To", PromptParams.To);
            await Task.CompletedTask;
        });
    }

    public bool HasFeedback(int clubID)
    {
        return PredicateExecuteScalar<bool>("select Found=1 from ClubFeedbacks where ClubID = @ClubID group by ClubID;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@ClubID", clubID);
            await Task.CompletedTask;
        });
    }

    public async Task<IEnumerable<string>> GetFeedbacks(int ClubID)
    {
        return await PredicateExecuteReaderAsync("select Feedback from ClubFeedbacks where ClubID = @ClubID order by CreatedAt;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@ClubID", ClubID);
            await Task.CompletedTask;
        },(SqlDataReader reader) =>
        {
            return reader.GetString(reader.GetOrdinal("Feedback"));
        });
    }
}
