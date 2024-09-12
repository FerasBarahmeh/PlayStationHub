using FluentValidation;
using PlayStationHub.Business.DataTransferObject.Users.Requests;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;

namespace PlayStationHub.API.Validators.User;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    private readonly IUserService _UserService;
    public UpdateUserValidator(IUserService UserService)
    {
        _UserService = UserService;
        RuleFor(u => u.Username)
             .NotNull().When(u => u.Username != null)
             .DependentRules(() =>
             {
                 RuleFor(u => u.Username)
                    .MinimumLength(5)
                    .MaximumLength(17)
                    .Must(_NotContainSpaces).WithMessage("Username must not contain spaces");
                 //.Must(_BeUniqueUsername).When(u => u.Username != null).WithMessage("Username is already used, chose another one");
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


        RuleFor(x => x.Status)
               .NotNull().When(u => u.Status != null)
             .DependentRules(() =>
             {
                 RuleFor(u => u.Status)
                       .Must(value => Enum.IsDefined(typeof(UserStatus), value))
                    .WithMessage("Invalid status value.");
             }).When(u => u.Status != null);

    }
    private bool _BeAValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.All(char.IsDigit);
    }
    private bool _BeUniqueUsername(string username)
    {
        return true;
    }
    private bool _NotContainSpaces(string username)
    {
        return !username.Contains(" ");
    }
}

