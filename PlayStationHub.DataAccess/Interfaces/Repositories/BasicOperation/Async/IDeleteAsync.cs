namespace PlayStationHub.DataAccess.Interfaces.Repositories.BasicOperation.Async;

public interface IDeleteAsync
{
    Task<int> DeleteAsync(int ID);
}
