using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using System.Data;
using System.Reflection.PortableExecutable;

namespace PlayStationHub.DataAccess.Repositories;

public class ClubRepository(IConfiguration configuration) : BaseRepository<Club>(configuration), IClubRepository
{
    public async Task<IEnumerable<Club>> AllAsync()
    {
        
        return await PredicateExecuteReaderAsync("select * from Clubs;", (reader) =>
        {
            return Club.GenerateOne(reader);
        });
    }
}

