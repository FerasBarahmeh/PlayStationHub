using AutoMapper;
using PlayStationHub.Business.Enums;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.Business.Profiles;

public class StatusNameResolver : IValueResolver<Club, ClubDto, string>
{
    public string Resolve(Club source, ClubDto destination, string destMember, ResolutionContext context)
    {
        if (source.Status == Convert.ToByte(EnmStatus.Active))
            return "active";
        else if (source.Status == Convert.ToByte(EnmStatus.Inactive))
            return "inactive";
        else
            return "unknown";
    }
}
public class ClubProfile : Profile
{
    public ClubProfile()
    {
        CreateMap<Club, ClubDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom<StatusNameResolver>());

        CreateMap<ClubDto, Club>();

        CreateMap<InsertClubDto, ClubDto>()
          .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new Owner { ID = src.OwnerID }));

        CreateMap<Club, ClubCoreDto>()
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Owner.User.ID))
            .ForMember(dest => dest.ClubID, opt => opt.MapFrom(src => src.ID));
    }
}
