namespace PlayStationHub.Business.Interfaces.Repositories.BasicOperation.Async;

public interface IFindAsync<T>
{
    Task<T> FindAsync(int ID);
}
