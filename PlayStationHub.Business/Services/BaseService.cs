using AutoMapper;

namespace PlayStationHub.Business.Services;

public abstract class BaseService<T>
{
    protected readonly T _Repository;
    protected IMapper _Mapper { get; set; }
    public BaseService(T Repository, IMapper mapper)
    {
        _Repository = Repository;
        _Mapper = mapper;
    }
}
