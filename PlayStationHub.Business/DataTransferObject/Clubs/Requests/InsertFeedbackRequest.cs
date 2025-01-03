﻿using PlayStationHub.Business.Enums;

namespace PlayStationHub.Business.DataTransferObject.Clubs.Requests;

public class InsertFeedbackRequest
{
    public string Feedback { get; set; }
    public int ClubID { get; set; }
    public byte Status  {  get { return (byte)GeneralStatus.Active; } }
    public DateTime CreatedAt { get { return DateTime.Now; } }
}
