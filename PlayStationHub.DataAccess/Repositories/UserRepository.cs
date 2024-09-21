using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using System.Data;

namespace PlayStationHub.DataAccess.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }
    public async Task<IEnumerable<User>> AllAsync()
    {
        return await PredicateExecuteReaderAsync("SELECT * FROM Users;", reader =>
        {
            return User.GenerateOne(reader);
        });
    }
    public async Task<bool> IsExistAsync(string Username)
    {
        return await PredicateExecuteScalarAsync<bool>("SELECT Founded = 1 FROM Users WhERE Username=@Username;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Username", Username);
            await Task.CompletedTask;
        });
    }
    public bool IsExist(string Username)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Users WhERE Username=@Username;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Username", Username);
            await Task.CompletedTask;
        });
    }
    public async Task<bool> IsExistAsync(int ID)
    {
        return await PredicateExecuteScalarAsync<bool>("SELECT Founded = 1 FROM Users WhERE ID=@ID;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@ID", ID);
            await Task.CompletedTask;
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
            return User.GenerateOne(reader);
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
    public async Task<int> InsertAsync(User user)
    {
        return await PredicateExecuteScalarAsync<int>("SP_InsertUser", async (SqlCommand cmd) =>
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            await Task.CompletedTask;
        });
    }
    public async Task<int> DeleteAsync(int ID)
    {
        return await PredicateExecuteNonQueryAsync("SP_DeleteUserByID", async (SqlCommand cmd) =>
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            await Task.CompletedTask;
        });
    }
    public bool IsExist(int ID)
    {
        return PredicateExecuteScalar<bool>("SELECT Founded = 1 FROM Users WhERE ID=@ID;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@ID", ID);
            await Task.CompletedTask;
        });
    }
    public async Task<User> GetUserCredentialsByUsernameAsync(string Username)
    {
        return await PredicateExecuteReaderForOneRecordAsync("SELECT TOP 1 * FROM Users WHERE Username=@Username;", async (SqlCommand cmd) =>
        {
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = Username;
            await Task.CompletedTask;

        }, reader =>
        {
            return User.GenerateOne(reader);
        });

    }
    public async Task<int> UpdateAsync(User Entity)
    {
        string Query = "UPDATE Users SET Username=@Username, Email=@Email, Phone=@Phone, Status=@Status WHERE ID = @ID;";
        return await PredicateExecuteNonQueryAsync(Query, async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@Username", Entity.Username);
            cmd.Parameters.AddWithValue("@Email", Entity.Email);
            cmd.Parameters.AddWithValue("@Phone", Entity.Phone);
            cmd.Parameters.AddWithValue("@Status", Entity.Status);
            cmd.Parameters.AddWithValue("@ID", Entity.ID);
            await Task.CompletedTask;
        });
    }
    public async Task<IEnumerable<UserPrivilege>> GetUserPrivilege(int id)
    {
        return await PredicateExecuteReaderAsync<UserPrivilege>("SP_GetUserPrivileges", async (SqlCommand cmd) =>
        {
            cmd.Parameters.AddWithValue("@UserID", id);
            await Task.CompletedTask;
        }, reader =>
        {
            return UserPrivilege.GenerateOne(reader);
        });
    }
}
