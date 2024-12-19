using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Repositories;

public class ClubRepository(IConfiguration configuration) : BaseRepository<Club>(configuration)
{
    int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Owner { get; set; }
    public int DeviceCount { get; set; }
}

