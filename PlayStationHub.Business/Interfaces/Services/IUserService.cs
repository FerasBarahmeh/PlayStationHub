using PlayStationHub.Business.DataTransferObject;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> AllAsync();
    Task<bool> IsExistAsync(string Username);
    Task<bool> IsExistAsync(int ID);
    Task<UserDTO> FindAsync(string Username);
    Task<UserDTO> FindAsync(int ID);
}
