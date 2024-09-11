using FluentValidation;
using PlayStationHub.Business.Interfaces.Services;

namespace PlayStationHub.API.Validators.User;

public class UserIDValidator : AbstractValidator<int>
{
    private readonly IUserService _UserService;

    public UserIDValidator(IUserService userService)
    {
        _UserService = userService;
        RuleFor(id => id)
            .Must(_IsUserExist).WithMessage("this user not exit in our credentials")
            .GreaterThan(0).WithMessage("ID must be greater than 0.");
    }
    private bool _IsUserExist(int ID)
    {
        return _UserService.IsExist(ID);
    }
}
