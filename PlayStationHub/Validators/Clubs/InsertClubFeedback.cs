using FluentValidation;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Requests.Clubs;

namespace PlayStationHub.API.Validators.Clubs;

public class InsertClubFeedback : AbstractValidator<InsertFeedbackDto>
{
    private readonly IClubFeedbackService _ClubFeedbackService;
    private readonly IClubService _ClubService;
    public InsertClubFeedback(IClubFeedbackService clubFeedbackService, IClubService clubservice)
    {
        _ClubService = clubservice;
        _ClubFeedbackService = clubFeedbackService;

        RuleFor(u => u.ClubID)
            .NotEmpty()
            .NotNull()
            .Must(_IsClubNotExist).WithMessage("This club not exist");

        RuleFor(u => u.Feedback)
        .NotNull()
        .NotEmpty()
        .Must(_BeUniqueUsername).WithMessage("This comment is already exist");

    }

    private bool _BeUniqueUsername(string username)
    {
        return !_ClubFeedbackService.IsExist(username);
    }
    private bool _IsClubNotExist(int clubID)
    {
        return _ClubService.IsExist(clubID);
    }
}
