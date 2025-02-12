using FluentValidation;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Requests.Clubs.interfaces;

namespace PlayStationHub.API.Validators.Clubs;

public class CheckIFClubExist : AbstractValidator<IID>
{
    private readonly IClubFeedbackService _ClubFeedbackService;
    public CheckIFClubExist(IClubFeedbackService clubFeedbackService)
    {
        _ClubFeedbackService = clubFeedbackService;
        RuleFor(u => u.ID)
        .NotNull()
        .NotEmpty()
        .Must(_IsClubHasFeedback).WithMessage("this club not exist or the club dosn't has feedback yet");
    }

    private bool _IsClubHasFeedback(int ClubID)
    {
        return _ClubFeedbackService.HasFeedback(ClubID);
    }
}
