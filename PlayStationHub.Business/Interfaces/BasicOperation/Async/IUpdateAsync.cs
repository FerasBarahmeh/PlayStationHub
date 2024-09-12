namespace PlayStationHub.Business.Interfaces.BasicOperation.Async;

public interface IUpdateAsync<T>
{
    Task<T> Update(T entity);
}
