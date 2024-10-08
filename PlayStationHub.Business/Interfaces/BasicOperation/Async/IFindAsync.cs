namespace PlayStationHub.Business.Interfaces.BasicOperation.Async;

public interface IFindAsync<T>
{
    Task<T> FindAsync(int ID);
}
