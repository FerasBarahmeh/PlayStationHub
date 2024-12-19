﻿namespace PlayStationHub.DataAccess.Entities;

public class Club
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public Owner Owner { get; set; }
    public int DeviceCount { get; set; }
}
