using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.Interfaces.Services;

public interface IUserService
{
    public ModeStatus Mode { get; }
    public string Password { get; set; }
    UserDTO UserModel { get; set; }
    Task<IEnumerable<UserDTO>> AllAsync();
    Task<bool> IsExistAsync(string Username);
    bool IsExist(string Username);
    Task<bool> IsExistAsync(int ID);
    Task<UserDTO> FindAsync(string Username);
    Task<UserDTO> FindAsync(int ID);
    Task<bool> SaveAsync();
}
