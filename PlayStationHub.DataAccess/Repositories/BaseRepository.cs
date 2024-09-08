using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PlayStationHub.DataAccess.Repositories;

public abstract class BaseRepository<T>
{
    protected delegate T ImplementExecuteReaderAsyncStructure();

    protected readonly string _ConnectionString;
    public BaseRepository(IConfiguration configuration)
    {
        _ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public virtual async Task<IEnumerable<T>> ReaderAllRecordsAsync(string Query, Func<SqlDataReader, T> Logic)
    {
        List<T> Users = new List<T>();
        using (SqlConnection conn = new SqlConnection(_ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        T user = Logic(reader);
                        Users.Add(user);
                    }
                }
            }
        }
        return Users;
    }
}
