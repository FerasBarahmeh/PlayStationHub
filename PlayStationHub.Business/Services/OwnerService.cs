using AutoMapper;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using PlayStationHub.DTOs.Owners;

namespace PlayStationHub.Business.Services;

public class OwnerService(IOwnerRepository repo, IMapper _Mapper) : BaseService<IOwnerRepository>(repo), IOwnerService
{
    public async Task<IEnumerable<OwnerCoreDetailsDto>> GetOwnersCoreDetailsAsync()
    {
        IEnumerable<Owner> Owners = await _Repository.GetOwnersCoreDetailsAsync();
        return _Mapper.Map<IEnumerable<OwnerCoreDetailsDto>>(Owners);
    }

    public bool IsExist(int ID)
    {
        return _Repository.IsExist(ID);
    }
}
