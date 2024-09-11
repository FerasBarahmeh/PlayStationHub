namespace PlayStationHub.Business.Interfaces.Repositories.BasicOperation.Async;

public interface IAllAsync<T>
{
    Task<IEnumerable<T>> AllAsync();
}
