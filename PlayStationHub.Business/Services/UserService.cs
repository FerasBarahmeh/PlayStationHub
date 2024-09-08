using AutoMapper;
using PlayStationHub.Business.DataTransferObject;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.Business.Services;

public class UserService : BaseService, IUserService
{
    public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper) { }

    public async Task<IEnumerable<UserDTO>> AllAsync()
    {
        var users = await _UserRepository.AllAsync();

        return _Mapper.Map<IEnumerable<UserDTO>>(users);
    }
}
