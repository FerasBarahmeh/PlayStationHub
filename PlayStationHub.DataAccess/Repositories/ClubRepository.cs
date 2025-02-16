using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Generators;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.DataAccess.Repositories;

public class ClubRepository(IConfiguration configuration) : BaseRepository<Club>(configuration), IClubRepository
{
    public async Task<IEnumerable<Club>> AllAsync()
    {
        return await PredicateExecuteReaderAsync("select * from vw_Clubs;", (reader) =>
        {
            return ClubEntityGenerator.Generate(reader);
        });
    }

    public async Task<Club> FindAsync(int ID)
    {
        return await PredicateExecuteReaderForOneRecordAsync("SELECT * FROM vw_Clubs where ClubID=@ClubID", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("ClubID", ID);
            await Task.CompletedTask;
        }, reader => ClubEntityGenerator.Generate(reader));
    }

    public async Task<int> InsertAsync(Club InsertField)
    {
        string sql = @"
        INSERT INTO Clubs(Name, Location, OwnerID, DeviceCount, ClubStatus) VALUES (@Name, @Location, @OwnerID,@DeviceCount,@ClubStatus);
        SELECT SCOPE_IDENTITY() AS ID;
        ";
        return await PredicateExecuteScalarAsync<int>(sql, async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Name", InsertField.Name);
            cmd.Parameters.AddWithValue("@Location", InsertField.Location);
            cmd.Parameters.AddWithValue("@OwnerID", InsertField.Owner.ID);
            cmd.Parameters.AddWithValue("@DeviceCount", InsertField.DeviceCount);
            cmd.Parameters.AddWithValue("@ClubStatus", InsertField.Status);
            await Task.CompletedTask;
        });

    }

    public bool IsExist(int ID)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Clubs Where ID=@ID;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@ID", ID);
            await Task.CompletedTask;
        });
    }
    public bool IsExist(string Name)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Clubs Where Name=@Name;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Name", Name);
            await Task.CompletedTask;
        });
    }
}

