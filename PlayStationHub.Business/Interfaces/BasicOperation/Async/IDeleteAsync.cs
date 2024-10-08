namespace PlayStationHub.Business.Interfaces.BasicOperation.Async;

public interface IDeleteAsync
{
    Task<bool> DeleteAsync(int ID);
}
