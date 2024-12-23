using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.Business.Services;

public class ClubService : BaseService<IClubRepository>, IClubService
{
    public ClubService(IClubRepository Repository) : base(Repository)
    {
    }

    public async Task<IEnumerable<ClubDTO>> AllAsync()
    {
        var clubs = await _Repository.AllAsync();
        return ClubMapper.ToClubDTO(clubs);
    }
}
