using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.Mappers;

public class ClubMapper
{
    public static IEnumerable<ClubDTO> ToClubDTO(IEnumerable<Club> clubs)
    {
        List<ClubDTO> result = new List<ClubDTO>();
        foreach (var club in clubs)
        {
            result.Add(new ClubDTO
            {
                ID = club.ID,
                Name = club.Name,
                DeviceCount = club.DeviceCount,
                OwnerID = club.OwnerID,
                Location = club.Location,
            });
        }
        return result;
    }
}
