namespace PlayStationHub.Business.Interfaces.Repositories.BasicOperation.Async;

public interface IIsExistAsync
{
    Task<bool> IsExistAsync(int ID);
}
