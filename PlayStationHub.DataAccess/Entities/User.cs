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
}
