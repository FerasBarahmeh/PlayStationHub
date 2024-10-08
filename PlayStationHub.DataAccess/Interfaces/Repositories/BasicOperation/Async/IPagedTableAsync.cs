namespace PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;

public interface IPagedTableAsync<T>
{
    Task<IEnumerable<T>> PagedTableAsync(int PageNumber, int PageSize);
}
