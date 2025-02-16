using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace PlayStationHub.DataAccess.Repositories;

public abstract class BaseRepository<T>
{
    protected readonly string _ConnectionString;
    public BaseRepository(IConfiguration configuration)
    {
        _ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Predicate execute reader
    public async Task<IEnumerable<T>> PredicateExecuteReaderAsync(string Query, Func<SqlDataReader, T> Logic)
    {
        List<T> Records = new List<T>();
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        T record = Logic(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }
    public async Task<IEnumerable<T>> PredicateExecuteReaderAsync(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> Logic)
    {
        List<T> Records = new List<T>();
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        T record = Logic(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }
    public async Task<IEnumerable<Entity>> PredicateExecuteReaderAsync<Entity>(string Query, Func<SqlCommand, Task> SetParameters, Func<SqlDataReader, Entity> Logic)
    {
        List<Entity> Records = new List<Entity>();
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await SetParameters(cmd);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Entity record = Logic(reader);
                        Records.Add(record);
                    }
                }
            }
        }
        return Records;
    }
    #endregion

    #region Predicate Execute Scalar
    public async Task<ReturnType> PredicateExecuteScalarAsync<ReturnType>(string Query, Action<SqlCommand> SetParameters)
    {
        ReturnType Result = default;
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                await conn.OpenAsync();
                object ScalerResult = await cmd.ExecuteScalarAsync();

                if (ScalerResult != null && ScalerResult != DBNull.Value)
                    Result = (ReturnType)Convert.ChangeType(ScalerResult, typeof(ReturnType));
            }
        }
        return Result;
    }
    public TReturnType PredicateExecuteScalar<TReturnType>(string query, Action<SqlCommand> setParameters)
    {
        TReturnType result = default;
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                setParameters(cmd);

                conn.Open();
                object scalarResult = cmd.ExecuteScalar();

                if (scalarResult != null && scalarResult != DBNull.Value)
                    result = (TReturnType)Convert.ChangeType(scalarResult, typeof(TReturnType));
            }
        }
        return result;
    }
    public async Task<ReturnType> PredicateExecuteScalarAsync<ReturnType>(string Query)
    {
        ReturnType Result = default;
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await conn.OpenAsync();
                object ScalerResult = await cmd.ExecuteScalarAsync();

                if (ScalerResult != null && ScalerResult != DBNull.Value)
                    Result = (ReturnType)Convert.ChangeType(ScalerResult, typeof(ReturnType));
            }
        }
        return Result;
    }
    #endregion

    #region Predicate execure reader for one recored
    public async Task<T> PredicateExecuteReaderForOneRecordAsync(string Query, Action<SqlCommand> SetParameters, Func<SqlDataReader, T> Logic)
    {
        T Record = default;
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParameters(cmd);
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Record = Logic(reader);
                    }
                }
            }
        }
        return Record;
    }
    #endregion

    #region Predicate execute non query
    public async Task<int> PredicateExecuteNonQueryAsync(string Query, Action<SqlCommand> SetParams)
    {
        int RowAffected = 0;

        await using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SetParams(cmd);
                await conn.OpenAsync();
                RowAffected = await cmd.ExecuteNonQueryAsync();
            }
        }
        return RowAffected;
    }
    #endregion

    #region Paged table
    public async Task<IEnumerable<T>> PagedTableAsync(string sql, int pageNumber, int pageSize, Func<SqlDataReader, T> reader)
    {
        return await PredicateExecuteReaderAsync("SP_TablePagination", async (SqlCommand cmd) =>
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Query", sql);
            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            await Task.CompletedTask;
        }, reader);
    }
    #endregion
}
