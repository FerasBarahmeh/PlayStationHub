using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Clubs.Requests;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Clubs;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FeedbackClubsController(IClubFeedbackService servic, IGeminiService _geminiService) : BaseController<IClubFeedbackService>(servic)
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Insert(InsertFeedbackRequest feedback)
    {
        int id = await _Service.InsertAsync(ClubFeedbackMapper.ToClubFeedbackDTO(feedback));
       return Ok(new ResponseOutcome<int>(data: id, status: HttpStatusCode.OK, "Successfully inserted comment"));
    }

    [HttpPost("GenerateSummary")]
    public async Task<IActionResult> GenerateSummary()
    {
        try
        {
            var result = await _geminiService.GenerateResponseAsync(7);
            return Ok(new { data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
