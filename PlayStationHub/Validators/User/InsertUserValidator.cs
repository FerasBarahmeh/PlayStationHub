using FluentValidation;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.User;
using Utilities.Validators;

namespace PlayStationHub.API.Validators.User;

public class InsertUserValidator : AbstractValidator<InsertUserDto>
{
    private readonly IUserService _UserService;
    public InsertUserValidator(IUserService UserService)
    {
        _UserService = UserService;
        RuleFor(u => u.Username)
        .NotNull()
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(17)
        .Must(StringValidator.NotContainSpaces).WithMessage("Username must not contain spaces")
        .Must(_BeUniqueUsername).WithMessage("Username is already used, chose another one");
    }

    private bool _BeUniqueUsername(string username)
    {
        return !_UserService.IsExist(username);
    }
}
