using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Generators;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.DataAccess.Repositories;

public class OwnerRepository(IConfiguration configuration) : BaseRepository<Owner>(configuration), IOwnerRepository
{
    public async Task<IEnumerable<Owner>> GetOwnersCoreDetailsAsync()
    {
        string Query = @"SELECT OwnerID, UserID, OwnerUsername from vw_Owners;";
        return await PredicateExecuteReaderAsync(Query, CoreOwnerGenerator.GenerateCoreOwnerDetails);
    }

    public bool IsExist(int ID)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Owners WhERE ID=@ID;", (SqlCommand cmd) => cmd.Parameters.AddWithValue("@ID", ID));
    }
}
