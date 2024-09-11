namespace PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;

public interface IFindAsync<T>
{
    Task<T> FindAsync(int ID);
}
