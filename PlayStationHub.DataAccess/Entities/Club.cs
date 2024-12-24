﻿using Microsoft.Data.SqlClient;

namespace PlayStationHub.DataAccess.Entities;

public class Club
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int OwnerID { get; set; }
    public byte DeviceCount { get; set; }
}
