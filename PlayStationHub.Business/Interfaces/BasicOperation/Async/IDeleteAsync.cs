namespace PlayStationHub.Business.Interfaces.Repositories.BasicOperation.Async;

public interface IDeleteAsync
{
    Task<bool> DeleteAsync(int ID);
}
