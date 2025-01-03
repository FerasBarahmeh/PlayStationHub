﻿using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.DataTransferObject.Users;

public class UserDTO
{
    public int? ID { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public byte Status { get; set; }
    public string StatusName
    {
        get
        {
            return Enum.GetName(typeof(UserStatus), Status);
        }
    }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? PhoneVerifiedAt { get; set; }
    public DateTime? EmailVerifiedAt { get; set; }
    public override string ToString()
    {
        return new
        {
            ID,
            Username,
            Phone,
            Email,
            CreatedAt,
            UpdatedAt,
            PhoneVerifiedAt,
            EmailVerifiedAt
        }.ToString();
    }
}
