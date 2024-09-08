using PlayStationHub.Business.DataTransferObject;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> AllAsync();
}
