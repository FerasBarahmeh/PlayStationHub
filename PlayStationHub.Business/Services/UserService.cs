using AutoMapper;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using PlayStationHub.DTOs.Privileges;
using PlayStationHub.DTOs.User;
using Utilities.Security;

namespace PlayStationHub.Business.Services;

public class UserService : BaseService<IUserRepository>, IUserService
{
    public EnmMode Mode => (User.ID == null) ? EnmMode.Insert : EnmMode.Update;
    public UserDto User { get; set; }

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
            if (Mode == EnmMode.Insert)
            {
                _Password = value;
                return;
            }
            _Password = null;
        }
    }
    private readonly IMapper _Mapper;
    public UserService(IUserRepository repo, IMapper mapper) : base(repo)
    {
        _Mapper = mapper;
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
    public async Task<UserDto> FindAsync(string Username)
    {
        User user = await _Repository.FindAsync(Username);
        return _Mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> FindAsync(int ID)
    {
        User user = await _Repository.FindAsync(ID);
        return _Mapper.Map<UserDto>(user);
    }
    private async Task<int?> _Insert()
    {
        InsertUserDto insertFields = _Mapper.Map<InsertUserDto>(User);
        insertFields.Password = Password;
        User user = _Mapper.Map<User>(insertFields);
        int? ID = await _Repository.InsertAsync(user);

        return ID;
    }
    private async Task<int?> _Update()
    {
        User user = _Mapper.Map<User>(User);
        int? ID = await _Repository.UpdateAsync(user);
        return ID;
    }
    public async Task<bool> SaveAsync()
    {
        if (Mode == EnmMode.Insert)
        {
            int? ID = await _Insert();
            User.ID = ID;
            return ID != null;
        }
        else if (Mode == EnmMode.Update)
        {
            int? RowsAffected = await _Update();
            if (RowsAffected is not null && RowsAffected > 0)
            {
                User user = await _Repository.FindAsync((int)User.ID);
                User = _Mapper.Map<UserDto>(user);
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

    public async Task<UserLoginDto> GetUserCredentialsByUsernameAsync(string Username)
    {
        User user = await _Repository.GetUserCredentialsByUsernameAsync(Username);
        return _Mapper.Map<UserLoginDto>(user);
    }
    public async Task<IEnumerable<UserPrivilegeDto>> GetUserPrivilege(int id)
    {
        IEnumerable<UserPrivilege> privileges = await _Repository.GetUserPrivilege(id);
        return _Mapper.Map<IEnumerable<UserPrivilegeDto>>(privileges);
    }

    public async Task<IEnumerable<UserDto>> PagedTableAsync(int PageNumber, int PageSize)
    {
        IEnumerable<User> users = await _Repository.PagedTableAsync(PageNumber, PageSize);
        return _Mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<int> CountRowsAsync()
    {
        return await _Repository.CountRowsAsync();
    }
}
