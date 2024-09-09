using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.DataAccess.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<User>> AllAsync()
    {
        return await PredicateExecuteReaderAsync("SELECT * FROM Users;", reader =>
        {
            return new User
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                Status = reader.GetByte(reader.GetOrdinal("Status"))
            };
        });
    }

    public async Task<bool> IsExistAsync(string Username)
    {
        return await PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Users WhERE Username=@Username;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Username", Username);
            await Task.CompletedTask;
        });
    }
    public async Task<bool> IsExistAsync(int ID)
    {
        return await PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Users WhERE ID=@ID;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@ID", ID);
        });
    }

    private async Task<User> _FindAsyncStructure(string Query, Func<SqlCommand, Task> Params)
    {
        return await PredicateExecuteReaderForOneRecordAsync(Query, async (SqlCommand cmd) =>
        {
            await Params(cmd);

            await Task.CompletedTask;
        }, reader =>
        {
            return new User
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                Password = reader.GetString(reader.GetOrdinal("Password")),
                Status = reader.GetByte(reader.GetOrdinal("Status"))
            };
        });
    }

    public async Task<User> FindAsync(int ID)
    {
        string Query = @"SELECT * FROM Users WHERE ID = @ID;";
        return await _FindAsyncStructure(Query, async (cmd) =>
        {
            cmd.Parameters.AddWithValue("@ID", ID);
            await Task.CompletedTask;
        });
    }
    public async Task<User> FindAsync(string Username)
    {
        string Query = @"SELECT * FROM Users WHERE Username = @Username;";
        return await _FindAsyncStructure(Query, async (cmd) =>
        {
            cmd.Parameters.AddWithValue("@Username", Username);
            await Task.CompletedTask;
        });
    }
}
