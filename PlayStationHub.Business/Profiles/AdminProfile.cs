using AutoMapper;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DTOs.Admins;

namespace PlayStationHub.Business.Profiles;

public class AdminProfile : Profile
{
    public AdminProfile()
    {

        CreateMap<AdminDto, Admin>().ReverseMap();
    }
}
