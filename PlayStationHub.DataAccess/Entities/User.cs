using Microsoft.Data.SqlClient;

namespace PlayStationHub.DataAccess.Entities;

public class User
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public byte Status { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? PhoneVerifiedAt { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public IEnumerable<UserPrivilege> Privileges { get; set; }
    public static User GenerateOne(SqlDataReader reader)
    {
        return new User
        {
            ID = reader.GetInt32(reader.GetOrdinal("ID")),
            Username = reader.GetString(reader.GetOrdinal("Username")),
            Phone = reader.GetString(reader.GetOrdinal("Phone")),
            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
            Password = reader.GetString(reader.GetOrdinal("Password")),
            Status = reader.GetByte(reader.GetOrdinal("Status")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
            PhoneVerifiedAt = reader.IsDBNull(reader.GetOrdinal("PhoneVerifiedAt"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("PhoneVerifiedAt")),
            EmailVerifiedAt = reader.IsDBNull(reader.GetOrdinal("EmailVerifiedAt"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("EmailVerifiedAt")),
        };
    }
}
