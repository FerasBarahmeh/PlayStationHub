namespace PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;

public interface IInsertAsync<T>
{
    Task<int> InsertAsync(T InsertField);
}
