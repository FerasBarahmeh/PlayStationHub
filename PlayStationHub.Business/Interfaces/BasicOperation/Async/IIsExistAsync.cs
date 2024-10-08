namespace PlayStationHub.Business.Interfaces.BasicOperation.Async;

public interface IIsExistAsync
{
    Task<bool> IsExistAsync(int ID);
}
