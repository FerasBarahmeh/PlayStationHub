using AutoMapper;
using PlayStationHub.Business.DataTransferObject;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.Business.Services;

public class UserService : BaseService<IUserRepository>, IUserService
{
    public UserService(IUserRepository repo, IMapper map) : base(repo, map) { }

    public async Task<IEnumerable<UserDTO>> AllAsync()
    {
        var users = await _Repository.AllAsync();

        return _Mapper.Map<IEnumerable<UserDTO>>(users);
    }
    public async Task<bool> IsExistAsync(string Username)
    {
        return await _Repository.IsExistAsync(Username);
    }
    public async Task<bool> IsExistAsync(int ID)
    {
        return await _Repository.IsExistAsync(ID);
    }
    public async Task<UserDTO> FindAsync(string Username)
    {
        var user = await _Repository.FindAsync(Username);
        return _Mapper.Map<UserDTO>(user);
    }
    public async Task<UserDTO> FindAsync(int ID)
    {
        var user = await _Repository.FindAsync(ID);
        return _Mapper.Map<UserDTO>(user);
    }
}
