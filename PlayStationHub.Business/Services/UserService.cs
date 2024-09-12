using AutoMapper;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using Utilities.Security;

namespace PlayStationHub.Business.Services;

public class UserService : BaseService<IUserRepository>, IUserService
{
    public ModeStatus Mode => (UserModel.ID == null) ? ModeStatus.Insert : ModeStatus.Update;
    public UserDTO UserModel { get; set; }
    private string _Password;
    public string Password
    {
        get
        {
            if (_Password != null)
                return Hashing.Hash(_Password);
            return null;
        }
        set
        {
            if (Mode == ModeStatus.Insert)
            {
                _Password = value;
                return;
            }
            _Password = null;
        }
    }
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
    public bool IsExist(string Username)
    {
        return _Repository.IsExist(Username);
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
    private async Task<int?> _Insert()
    {
        var userForCreationDto = _Mapper.Map<UserForCreationDTO>(UserModel);

        userForCreationDto.Password = Password;

        int? ID = await _Repository.InsertAsync(_Mapper.Map<User>(userForCreationDto));

        return ID;
    }
    public async Task<int?> _Update()
    {
        var user = _Mapper.Map<User>(UserModel);
        int? ID = await _Repository.UpdateAsync(user);
        return ID;
    }
    public async Task<bool> SaveAsync()
    {
        if (Mode == ModeStatus.Insert)
        {
            int? ID = await _Insert();
            UserModel.ID = ID;
            return ID != null;
        }
        else if (Mode == ModeStatus.Update)
        {
            int? RowsAffected = await _Update();
            if (RowsAffected is not null && RowsAffected > 0)
            {
                var user = await _Repository.FindAsync((int)UserModel.ID);
                UserModel = _Mapper.Map<UserDTO>(user);
                return true;
            }
        }
        return false;

    }

    public async Task<bool> DeleteAsync(int ID)
    {
        return await _Repository.DeleteAsync(ID) > 0;
    }

    public bool IsExist(int ID)
    {
        return _Repository.IsExist(ID);
    }

    public async Task<UserLoginDTO> GetUserCredentialsByUsernameAsync(string Username)
    {
        var user = await _Repository.GetUserCredentialsByUsernameAsync(Username);
        return _Mapper.Map<UserLoginDTO>(user);
    }


}
