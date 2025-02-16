using AutoMapper;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.Owners;

namespace PlayStationHub.Business.Profiles;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        CreateMap<OwnerDto, Owner>().ReverseMap();
        CreateMap<Owner, OwnerCoreDetailsDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.User.ID));

    }
}
