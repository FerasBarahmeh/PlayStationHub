using Microsoft.Data.SqlClient;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.DataAccess.Generators;

public class UserEntityGenerator
{
    public static User Generate(SqlDataReader reader)
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

    public static User Generate(SqlDataReader reader, string columnPrefix)
    {
        return new User
        {
            ID = reader.GetInt32(reader.GetOrdinal(columnPrefix+"UserID")),
            Username = reader.GetString(reader.GetOrdinal(columnPrefix + "Username")),
            Phone = reader.GetString(reader.GetOrdinal(columnPrefix + "Phone")),
            Email = reader.GetString(reader.GetOrdinal(columnPrefix + "Email")),
            Status = reader.GetByte(reader.GetOrdinal(columnPrefix + "Status")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal(columnPrefix + "CreatedAt")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal(columnPrefix + "UpdatedAt")),
            EmailVerifiedAt = reader.IsDBNull(reader.GetOrdinal(columnPrefix + "EmailVerifiedAt"))
                                          ? null
                                          : reader.GetDateTime(reader.GetOrdinal(columnPrefix + "EmailVerifiedAt")),
            PhoneVerifiedAt = reader.IsDBNull(reader.GetOrdinal(columnPrefix + "PhoneVerifiedAt"))
                                          ? null
                                          : reader.GetDateTime(reader.GetOrdinal(columnPrefix + "PhoneVerifiedAt")),
        };
    }
}
