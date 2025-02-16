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
        return await PredicateExecuteReaderAsync("select * from vw_Clubs where ClubStatus = 2;", ClubEntityGenerator.Generate);
    }

    public async Task<Club> FindAsync(int ID)
    {
        return await PredicateExecuteReaderForOneRecordAsync(
            "SELECT * FROM vw_Clubs where ClubID=@ClubID",
            (SqlCommand cmd) => cmd.Parameters.AddWithValue("ClubID", ID),
            ClubEntityGenerator.Generate
        );
    }

    public async Task<IEnumerable<Club>> GetUserClubsHighlights(int UserID)
    {
        return await PredicateExecuteReaderAsync(
            "SELECT * FROM vw_GetActiveClubs WHERE OwnerUserID = @OwnerUserID;",
            (SqlCommand cmd) => cmd.Parameters.AddWithValue("@OwnerUserID", UserID),
            ClubEntityGenerator.Generate
        );
    }

    public async Task<int> InsertAsync(Club InsertField)
    {
        string sql = @"
        INSERT INTO Clubs(Name, Location, OwnerID, DeviceCount, ClubStatus) VALUES (@Name, @Location, @OwnerID,@DeviceCount,@ClubStatus);
        SELECT SCOPE_IDENTITY() AS ID;
        ";
        return await PredicateExecuteScalarAsync<int>(sql, (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Name", InsertField.Name);
            cmd.Parameters.AddWithValue("@Location", InsertField.Location);
            cmd.Parameters.AddWithValue("@OwnerID", InsertField.Owner.ID);
            cmd.Parameters.AddWithValue("@DeviceCount", InsertField.DeviceCount);
            cmd.Parameters.AddWithValue("@ClubStatus", InsertField.Status);
        });

    }

    public bool IsExist(int ID)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Clubs Where ID=@ID;", (SqlCommand cmd) => cmd.Parameters.AddWithValue("@ID", ID));
    }
    public bool IsExist(string Name)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Clubs Where Name=@Name;", (SqlCommand cmd) => cmd.Parameters.AddWithValue("@Name", Name));
    }
    public async Task<int> SoftDeleteAsync(int ID)
    {
        return await PredicateExecuteNonQueryAsync("UPDATE Clubs SET ClubStatus = 1 WHERE ID = @ID AND ClubStatus != 1;", (SqlCommand cmd) => cmd.Parameters.AddWithValue("@ID", ID));
    }
}

