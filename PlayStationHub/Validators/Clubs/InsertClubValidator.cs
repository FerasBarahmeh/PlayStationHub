using FluentValidation;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.Clubs;
using Utilities.Validators;

namespace PlayStationHub.API.Validators.Clubs;

public class InsertClubValidator : AbstractValidator<InsertClubDto>
{
    private readonly IClubService _ClubService;
    private readonly IOwnerService _OwnerService;

    public InsertClubValidator(IClubService ClubService, IOwnerService ownerService)
    {
        _ClubService = ClubService;
        _OwnerService = ownerService;

        RuleFor(owner => owner.Name)
        .NotNull()
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(100)
        .Must(_BeUniqueClubName).WithMessage("chose unique name for club, this name already used.");

        RuleFor(owner => owner.Location)
        .NotNull()
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(100);

        RuleFor(owner => owner.OwnerID)
        .NotNull()
        .NotEmpty()
        .GreaterThan(0).WithMessage("OwnerID must be greater than 0.")
        .Must(_IsOwnerExist).WithMessage("the owner is not exist");

        RuleFor(owner => owner.Status)
            .NotNull()
            .NotEmpty()
            .Must(EnumsValidator.IsDefinedInEnum<EnmStatus, byte>).WithMessage("in valid status value");
    }

    private bool _BeUniqueClubName(string Name)
    {
        return _ClubService.IsExist(Name);
    }
    private bool _IsOwnerExist(int ID)
    {
        return _OwnerService.IsExist(ID);
    }
}
