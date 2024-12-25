using FluentValidation;
using PlayStationHub.Business.DataTransferObject.Clubs.Requests;
using PlayStationHub.Business.Interfaces.Services;

namespace PlayStationHub.API.Validators.Clubs;

public class InsertClubFeedback : AbstractValidator<InsertFeedbackRequest>
{
    private readonly IClubFeedbackService _ClubFeedbackService;
    public InsertClubFeedback(IClubFeedbackService clubFeedbackService)
    {
        _ClubFeedbackService = clubFeedbackService;
        RuleFor(u => u.Feedback)
        .NotNull()
        .NotEmpty()
        .Must(_BeUniqueUsername).WithMessage("This comment is already exist");
    }

    private bool _BeUniqueUsername(string username)
    {
        return !_ClubFeedbackService.IsExist(username);
    }
}
