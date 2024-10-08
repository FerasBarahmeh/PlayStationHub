namespace PlayStationHub.Business.Interfaces.BasicOperation.Async;

public interface IInsertAsync<T>
{
    Task<int> InsertAsync(T InsertField);
}
