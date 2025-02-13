using AutoMapper;
using PlayStationHub.Business.Enums;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.User;

namespace PlayStationHub.Business.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Bidirectional Mapping (User <-> UserDto)
        CreateMap<User, UserDto>()
        .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src =>
          src.Status == (byte)EnmStatus.Active ? "active" :
          src.Status == (byte)EnmStatus.Inactive ? "inactive" :
          "unknown"
        ));

        CreateMap<UserDto, User>();

        // Other DTOs Mapping
        CreateMap<User, UserLoginDto>().ReverseMap();
        CreateMap<User, InsertUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();

        // DTO to DTO Mappings
        CreateMap<InsertUserDto, UserDto>().ReverseMap();

        CreateMap<UpdateUserDto, UserDto>()
         .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Updates only non-null values

    }
}
