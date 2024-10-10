namespace PlayStationHub.Utilities.Interfaces.BasicOperation.Async;

public interface IPagedTableAsync<T> where T : class
{
    Task<IEnumerable<T>> PagedTableAsync(int PageNumber, int PageSize);
}
