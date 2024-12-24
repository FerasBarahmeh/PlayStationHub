using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.DataAccess.Repositories;

public class ClubFeedbackRepository(IConfiguration configuration) : BaseRepository<Club>(configuration), IClubFeedback
{
    public Task<int> InsertAsync(ClubFeedback InsertField)
    {
        throw new NotImplementedException();
    }
}
