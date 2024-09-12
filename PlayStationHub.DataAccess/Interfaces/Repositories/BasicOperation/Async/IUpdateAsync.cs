namespace PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;

public interface IUpdateAsync<T>
{
    Task<int> UpdateAsync(T entity);
}
