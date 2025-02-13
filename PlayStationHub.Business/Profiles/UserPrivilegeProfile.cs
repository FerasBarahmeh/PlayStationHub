using AutoMapper;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.Privileges;

namespace PlayStationHub.Business.Profiles;

public class UserPrivilegeProfile : Profile
{
    public UserPrivilegeProfile()
    {
        CreateMap<UserPrivilege, UserPrivilegeDto>().ReverseMap();
    }
}
