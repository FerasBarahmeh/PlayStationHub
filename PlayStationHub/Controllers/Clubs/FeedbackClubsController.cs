using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using PlayStationHub.Business.Requests.Clubs;
using System.Net;
using System.Text.Json;
using Utilities.Response;
using Utilities.Response.ThirdParty;

namespace PlayStationHub.API.Controllers.Clubs;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(Privileges.Owner))]
public class FeedbackClubsController(IClubFeedbackService servic, IGeminiService _geminiService) : BaseController<IClubFeedbackService>(servic)
{
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Insert(InsertFeedbackRequest feedback)
    {
        int id = await _Service.InsertAsync(ClubFeedbackMapper.ToClubFeedbackDTO(feedback));
        return Ok(new ResponseOutcome<int>(data: id, status: HttpStatusCode.OK, "Successfully inserted comment"));
    }

    [HttpPost("GetFeedbacks")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult> GetFeedbacks(GetFeedbacksForClubRequest request)
    {
        IEnumerable<string> comments = await _Service.GetFeedbacks(request.ID);

        return Ok(new ResponseOutcome<IEnumerable<string>>(data: comments.ToList(), HttpStatusCode.OK, message: "customers feedback"));
    }

    [HttpPost("GenerateSummary")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> GenerateSummary(GenerateSummaryForCommentsToClubRequest request)
    {
        try
        {
            var result = await _geminiService.GenerateResponseAsync(ClubFeedbackMapper.ToPromptParamsDto(request));

            var jsonObject = JsonDocument.Parse(result);

            var textContent = jsonObject
                .RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            var usageMetadata = jsonObject.RootElement.GetProperty("usageMetadata");

            var tokenInfo = new Dictionary<string, object>
            {
                { "PromptTokenCount", usageMetadata.GetProperty("promptTokenCount").GetInt32() },
                { "CandidatesTokenCount", usageMetadata.GetProperty("candidatesTokenCount").GetInt32() },
                { "TotalTokenCount",  usageMetadata.GetProperty("totalTokenCount").GetInt32() }
            };

            Metadata metadata = new Metadata(data: tokenInfo);

            return Ok(new ResponseOutcome<string>(data: textContent, HttpStatusCode.OK, metadata: metadata, message: ""));
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
