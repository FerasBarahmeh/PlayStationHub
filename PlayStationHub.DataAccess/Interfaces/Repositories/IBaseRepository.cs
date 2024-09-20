using Microsoft.Data.SqlClient;

namespace PlayStationHub.DataAccess.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> PredicateExecuteReaderAsync(string query, Func<SqlDataReader, T> logic);
    Task<IEnumerable<T>> PredicateExecuteReaderAsync(string query, Func<SqlCommand, Task> setParameters, Func<SqlDataReader, T> logic);
    Task<IEnumerable<TEntity>> PredicateExecuteReaderAsync<TEntity>(string query, Func<SqlCommand, Task> setParameters, Func<SqlDataReader, TEntity> logic);
    Task<T> PredicateExecuteReaderForOneRecordAsync(string query, Func<SqlCommand, Task> setParameters, Func<SqlDataReader, T> logic);
    Task<TReturnType> PredicateExecuteScalarAsync<TReturnType>(string query, Func<SqlCommand, Task> setParameters);
    TReturnType PredicateExecuteScalar<TReturnType>(string query, Action<SqlCommand> setParameters);
    Task<int> PredicateExecuteNonQueryAsync(string query, Func<SqlCommand, Task> setParameters);
}
