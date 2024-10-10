namespace PlayStationHub.Utilities.Interfaces.BasicOperation.Async;

public interface IAllAsync<T>
{
    Task<IEnumerable<T>> AllAsync();
}
