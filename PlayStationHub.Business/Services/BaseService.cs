using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.Services;

public abstract class BaseService<T>
{
    protected ModeStatus _Mode = ModeStatus.Insert;

    protected readonly T _Repository;
    public BaseService(T Repository)
    {
        _Repository = Repository;
    }
}
