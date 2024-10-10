namespace PlayStationHub.Utilities.Interfaces.BasicOperation.Async;

public interface IUpdateAsync<T>
{
    Task<int> UpdateAsync(T entity);
}
