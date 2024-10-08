namespace PlayStationHub.Business.Interfaces.BasicOperation.Async;

public interface PagedTableAsync<T> where T : class
{
    Task<IEnumerable<T>> PagedTableAsync(int PageNumber, int PageSize);
}
