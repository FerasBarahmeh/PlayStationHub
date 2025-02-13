using AutoMapper;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.Owners;

namespace PlayStationHub.Business.Profiles;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        CreateMap<OwnerDto, Owner>().ReverseMap();
    }
}
