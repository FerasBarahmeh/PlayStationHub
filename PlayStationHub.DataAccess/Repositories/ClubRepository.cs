using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Generatories;
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
}

