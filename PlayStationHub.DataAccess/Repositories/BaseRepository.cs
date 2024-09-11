﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PlayStationHub.DataAccess.Repositories;

public abstract class BaseRepository<T>
{
    protected readonly string _ConnectionString;
    public BaseRepository(IConfiguration configuration)
    {
        _ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

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
    public async Task<T> PredicateExecuteReaderForOneRecordAsync(string Query, Func<SqlCommand, Task> SetParameters, Func<SqlDataReader, T> Logic)
    {
        T Record = default;
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
                        Record = Logic(reader);
                    }
                }
            }
        }
        return Record;
    }

    public async Task<ReturnType> PredicateExecuteScalarAsync<ReturnType>(string Query, Func<SqlCommand, Task> SetParameters)
    {
        ReturnType Result = default;
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await SetParameters(cmd);
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

    public async Task<int> PredicateExecuteNonQuery(string Query, Func<SqlCommand, Task> SetParams)
    {
        int RowAffected = 0;

        await using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await SetParams(cmd);
                await conn.OpenAsync();
                RowAffected = await cmd.ExecuteNonQueryAsync();
            }
        }
        return RowAffected;
    }
}
