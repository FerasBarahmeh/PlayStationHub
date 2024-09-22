using PlayStationHub.Business.DataTransferObject.Privileges;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.Mappers;

public class UserPrivilegeMapper
{

    public static IEnumerable<UserPrivilegeDTO> ToUserPrivilegeDTO(IEnumerable<UserPrivilege> privileges)
    {
        return privileges.Select(privilege => new UserPrivilegeDTO
        {
            ID = privilege.ID,
            UserID = privilege.UserID,
            PrivilegeID = privilege.PrivilegeID,
            Name = privilege.Name,
        }).ToList();
        //List<UserPrivilegeDTO> result = new List<UserPrivilegeDTO>();
        //foreach (var privilege in privileges)
        //{
        //    result.Add(new UserPrivilegeDTO
        //    {
        //        ID = privilege.ID,
        //        UserID = privilege.UserID,
        //        PrivilegeID = privilege.PrivilegeID,
        //        Name = privilege.Name,
        //    });
        //}
        //return result;
    }
}
