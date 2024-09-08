using AutoMapper;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.Business.Services;

public abstract class BaseService
{
    protected readonly IUserRepository _UserRepository;
    protected IMapper _Mapper { get; set; }
    public BaseService(IUserRepository userRepository, IMapper mapper)
    {
        _UserRepository = userRepository;
        _Mapper = mapper;
    }
}
