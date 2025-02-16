using AutoMapper;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.Business.Services;

public class ClubService(IClubRepository Repository, IMapper _Mapper) : BaseService<IClubRepository>(Repository), IClubService
{
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
    public bool IsExist(string Name)
    {
        return _Repository.IsExist(Name);
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
    public async Task<int> SoftDeleteAsync(int ID)
    {
        return await _Repository.SoftDeleteAsync(ID);
    }
    public async Task<IEnumerable<ClubDto>> GetUserClubs(int UserID)
    {
        IEnumerable<Club> Clubs = await _Repository.GetUserClubs(UserID);
        return _Mapper.Map<IEnumerable<ClubDto>>(Clubs);
    }
}
