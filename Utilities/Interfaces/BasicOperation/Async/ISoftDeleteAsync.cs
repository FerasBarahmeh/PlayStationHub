namespace Utilities.Interfaces.BasicOperation.Async;

public interface ISoftDeleteAsync
{
    Task<int> SoftDeleteAsync(int ID);
}
