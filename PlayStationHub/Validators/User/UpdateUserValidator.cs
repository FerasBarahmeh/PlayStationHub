using FluentValidation;
using PlayStationHub.API.Authentication;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.User;

namespace PlayStationHub.API.Validators.User;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    private readonly IUserService _UserService;
    private readonly ClaimsHelper _ClaimsHelper;
    public UpdateUserValidator(IUserService UserService, ClaimsHelper claimsHelper)
    {
        _UserService = UserService;
        _ClaimsHelper = claimsHelper;
        RuleFor(u => u.Username)
             .NotNull().When(u => u.Username != null)
             .DependentRules(() =>
             {
                 RuleFor(u => u.Username)
                    .MinimumLength(5)
                    .MaximumLength(17)
                    .Must(_NotContainSpaces).WithMessage("Username must not contain spaces")
                    .Must(_BeUniqueUsername).When(u => u.Username != null).WithMessage("Username is already used, chose another one");
             }).When(u => u.Username != null);


        RuleFor(u => u.Email)
             .NotNull().When(u => u.Email != null)
             .DependentRules(() =>
             {
                 RuleFor(u => u.Email)
                     .EmailAddress()
                     .WithMessage("Enter valid email");
             }).When(u => u.Email != null);


        RuleFor(u => u.Phone)
            .NotNull().When(u => u.Phone != null)
             .DependentRules(() =>
             {
                 RuleFor(u => u.Phone)
                      .Must(_BeAValidPhoneNumber).WithMessage("Phone number is not valid.");
             }).When(u => u.Phone != null);
    }
    private bool _BeAValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(char.IsDigit);
    }
    private bool _BeUniqueUsername(string username)
    {
        return !(_ClaimsHelper.Username != username && _UserService.IsExist(username));
    }
    private bool _NotContainSpaces(string username)
    {
        return !username.Contains(" ");
    }
}

