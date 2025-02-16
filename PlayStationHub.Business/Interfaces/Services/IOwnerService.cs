using PlayStationHub.DTOs.Owners;
using PlayStationHub.Utilities.Interfaces.BasicOperation.Sync;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IOwnerService : IIsExist
{
    Task<IEnumerable<OwnerCoreDetailsDto>> GetOwnersCoreDetailsAsync();
}
