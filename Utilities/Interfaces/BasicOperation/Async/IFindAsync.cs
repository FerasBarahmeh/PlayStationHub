namespace PlayStationHub.Utilities.Interfaces.BasicOperation.Async;

public interface IFindAsync<T>
{
    Task<T> FindAsync(int ID);
}
