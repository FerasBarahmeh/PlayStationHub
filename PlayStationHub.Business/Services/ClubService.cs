using AutoMapper;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.Business.Services;

public class ClubService(IClubRepository Repository, IMapper _Mapper) : BaseService<IClubRepository>(Repository), IClubService
{
    private EnmMode _mode => Club == null ? EnmMode.Insert : EnmMode.Update;

    public ClubDto Club { get; set; }

    public async Task<IEnumerable<ClubDto>> AllAsync()
    {
        IEnumerable<Club> clubs = await _Repository.AllAsync();
        return _Mapper.Map<IEnumerable<ClubDto>>(clubs);
    }

    public async Task<ClubDto> FindAsync(int ID)
    {
        Club club = await _Repository.FindAsync(ID);
        return _Mapper.Map<ClubDto>(club);
    }

    public bool IsExist(int ID)
    {
        return _Repository.IsExist(ID);
    }

    private async Task<int> _Insert()
    {
        Club club = _Mapper.Map<Club>(Club);
        int id = await _Repository.InsertAsync(club);
        return id;
    }

    public async Task<bool> SaveAsync()
    {
        switch (_Mode)
        {
            case EnmMode.Insert:
                int id = await _Insert();
                if (id == null) return false;
                Club = await FindAsync(id);
                _Mode = EnmMode.Update;
                return true;
        }
        return false;
    }
}
