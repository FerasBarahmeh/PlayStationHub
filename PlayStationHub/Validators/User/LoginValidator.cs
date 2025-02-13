using FluentValidation;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.Authentications;

namespace PlayStationHub.API.Validators.User;

public class LoginValidator : AbstractValidator<LoginDto>
{
    private readonly IUserService _UserService;
    public LoginValidator(IUserService userService)
    {
        _UserService = userService;
        RuleFor(u => u.Username)
        .NotNull()
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(17)
        .Must(_NotContainSpaces).WithMessage("Username must not contain spaces");
    }

    private bool _NotContainSpaces(string username)
    {
        return !username.Contains(" ");
    }
}
