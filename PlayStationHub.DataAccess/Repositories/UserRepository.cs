using Microsoft.Extensions.Configuration;
using PlayStationHub.DataAccess.Entities;
using PlayStationHub.DataAccess.Interfaces.Repositories;

namespace PlayStationHub.DataAccess.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<User>> AllAsync()
    {
        return await ReaderAllRecordsAsync("SELECT * FROM Users;", reader =>
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
}
