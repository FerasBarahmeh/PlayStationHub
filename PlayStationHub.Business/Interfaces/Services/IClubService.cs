using PlayStationHub.DTOs.Clubs;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IClubService : IAllAsync<ClubDto>, IIsExist, IFindAsync<ClubDto>, ISaveAsync
{
    ClubDto Club { get; set; }
}
