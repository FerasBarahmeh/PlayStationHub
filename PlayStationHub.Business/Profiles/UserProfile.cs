using AutoMapper;
using PlayStationHub.Business.DataTransferObject;
using PlayStationHub.Business.Enums;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
            .ForMember(destination => destination.Status, option =>
                option.MapFrom(src =>
                src.Status == (byte)UserStatus.Inactive ? nameof(UserStatus.Inactive) :
                src.Status == (byte)UserStatus.Active ? nameof(UserStatus.Active) :
                UserStatus.Unknown.ToString()
            ))
            .ReverseMap()
              .ForMember(destination => destination.Status, option =>
                option.MapFrom(src =>
                    src.Status == nameof(UserStatus.Inactive) ? (byte)UserStatus.Inactive :
                    src.Status == nameof(UserStatus.Active) ? (byte)UserStatus.Active
                    : (byte)UserStatus.Unknown
              ));
    }
}
