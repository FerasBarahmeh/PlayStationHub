using PlayStationHub.DTOs.Clubs;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Async;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;
using Utilities.Interfaces.BasicOperation.Async;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IClubService : IAllAsync<ClubDto>, IIsExist, IIsExistByContent, IFindAsync<ClubDto>, ISaveAsync, ISoftDeleteAsync
{
    ClubDto Club { get; set; }
}
