using FluentValidation;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.Clubs;

namespace PlayStationHub.API.Validators.Clubs;

public class CheckIfClubExist : AbstractValidator<ClubIDDto>
{
    private readonly IClubService _ClubService;
    public CheckIfClubExist(IClubService clubService)
    {
        _ClubService = clubService;

        RuleFor(u => u.ID)
        .NotNull()
        .NotEmpty()
        .Must(_IsClubHasFeedback).WithMessage("this club not exist or the club");
    }

    private bool _IsClubHasFeedback(int ClubID)
    {
        return _ClubService.IsExist(ClubID);
    }
}
