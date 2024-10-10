namespace PlayStationHub.Utilities.Interfaces.BasicOperation.Async;

public interface IDeleteAsync<T>
{
    Task<T> DeleteAsync(int ID);
}
