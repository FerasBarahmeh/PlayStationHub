using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.DataTransferObject.Clubs.Requests;
using PlayStationHub.Business.DataTransferObject.Users.Requests;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Clubs;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FeedbackClubsController(IClubFeedbackService servic) : BaseController<IClubFeedbackService>(servic)
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Insert(InsertFeedbackRequest feedback)
    {
        int id = await _Service.InsertAsync(ClubFeedbackMapper.ToClubFeedbackDTO(feedback));
       return Ok(new ResponseOutcome<int>(data: id, status: HttpStatusCode.OK, "Successfully inserted comment"));
    }
}
