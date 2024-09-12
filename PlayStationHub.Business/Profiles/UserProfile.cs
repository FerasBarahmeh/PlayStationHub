using AutoMapper;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.DataTransferObject.Users.Requests;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
            .ReverseMap();

        CreateMap<User, UserLoginDTO>()
         .ReverseMap();

        CreateMap<UserDTO, InsertUserRequest>()
            .ReverseMap();

        CreateMap<User, UserForCreationDTO>()
            .ReverseMap();

        CreateMap<UserDTO, UserForCreationDTO>()
            .ReverseMap();

        CreateMap<UpdateUserRequest, UserDTO>()
             .ForAllMembers(opts => opts.Condition((src, destination, srcMember) => srcMember != null));
    }
}
