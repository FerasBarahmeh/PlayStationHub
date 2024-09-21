using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.DataTransferObject.Users.Requests;
using PlayStationHub.DataAccess.Entities;

namespace PlayStationHub.Business.Mappers;

public class UserMapper
{
    public static User ToUser(UserDTO userDTO)
    {
        return new User
        {
            ID = (int)userDTO.ID,
            Username = userDTO.Username,
            Phone = userDTO.Phone,
            Email = userDTO.Email,
            Status = userDTO.Status,
            Password = default,
            CreatedAt = userDTO.CreatedAt,
            UpdatedAt = userDTO.UpdatedAt,
            PhoneVerifiedAt = userDTO.PhoneVerifiedAt,
            EmailVerifiedAt = userDTO.EmailVerifiedAt,
            Privileges = default
        };
    }
    public static User ToUser(UserLoginDTO userLoginDTO)
    {
        return new User
        {
            ID = default,
            Username = userLoginDTO.Username,
            Phone = default,
            Email = default,
            Status = default,
            Password = userLoginDTO.Password
        };
    }
    public static User ToUser(UserForCreationDTO userLoginDTO)
    {
        return new User
        {
            ID = default,
            Username = userLoginDTO.Username,
            Phone = userLoginDTO.Phone,
            Email = default,
            Status = default,
            Password = userLoginDTO.Password
        };
    }

    public static UserDTO ToUserDTO(User user)
    {
        return new UserDTO
        {
            ID = user.ID,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status,
            Phone = user.Phone,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            PhoneVerifiedAt = user.PhoneVerifiedAt,
            EmailVerifiedAt = user.EmailVerifiedAt,

        };
    }
    public static IEnumerable<UserDTO> ToUserDTO(IEnumerable<User> users)
    {
        List<UserDTO> result = new List<UserDTO>();
        foreach (var user in users)
        {
            result.Add(new UserDTO
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                PhoneVerifiedAt = user.PhoneVerifiedAt,
                EmailVerifiedAt = user.EmailVerifiedAt,

            });
        }
        return result;


    }
    public static UserDTO ToUserDTO(InsertUserRequest user)
    {
        return new UserDTO
        {
            ID = default,
            Username = user.Username,
            Phone = user.Phone,
        };
    }
    public static UserDTO MargeUserDtoWithUpdateRequest(UpdateUserRequest userRequest, UserDTO user)
    {
        return new UserDTO
        {
            ID = user.ID,
            Username = user.Username != userRequest.Username ? userRequest.Username : user.Username,
            Phone = user.Phone != userRequest.Phone ? userRequest.Phone : user.Phone,
            Email = user.Email != userRequest.Email ? userRequest.Email : user.Email,
        };
    }

    public static UserLoginDTO ToUserLoginDTO(User user)
    {
        return new UserLoginDTO
        {
            Username = user.Username,
            Password = user.Password
        };
    }

    public static UserForCreationDTO ToUserForCreation(UserDTO user)
    {
        return new UserForCreationDTO
        {
            Password = default,
            Username = user.Username,
            Phone = user.Phone,
        };
    }

}
