using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Requests.Clubs;
using PlayStationHub.DTOs.Clubs;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Clubs;

[Route("api/[controller]")]
[ApiController]
public class ClubsController(IClubService service, IMapper _Mapper) : BaseController<IClubService>(service)
{
    [HttpGet]
    public async Task<ActionResult> All()
    {
        var clubs = await _Service.AllAsync();
        if (clubs == null) return NoContent();

        return Ok(new ResponseOutcome<IEnumerable<ClubDto>>(data: clubs, status: HttpStatusCode.OK, message: "Success fetch clubs"));
    }

    [HttpPost("Insert")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = nameof(Privileges.Admin))]
    public async Task<ActionResult> Insert(InsertClubDto insertClubDto)
    {
        _Service.Club = _Mapper.Map<ClubDto>(insertClubDto);
        _Service.Club.Status = Convert.ToByte(EnmStatus.Active);
        bool isSaved = await _Service.SaveAsync();
        if (isSaved)
        {
            return Ok(new ResponseOutcome<ClubDto>(data: _Service.Club, status: HttpStatusCode.OK, message: $"Success inset club has id {_Service.Club.ID}"));
        }

        return Forbid();
    }

    [HttpPost("Find")]
    public async Task<ActionResult> Find(FindClubDto request)
    {
        var club = await _Service.FindAsync(request.ID);

        return Ok(new ResponseOutcome<ClubDto>(data: club, status: HttpStatusCode.OK, message: "Success fetch clubs"));
    }

    [HttpPatch("SoftDelete")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize(Roles = nameof(Privileges.Admin))]
    public async Task<ActionResult> SoftDelete(ClubIDDto ID)
    {
        try
        {
            int rowAffected = await _Service.SoftDeleteAsync(ID.ID);
            if (rowAffected == 1)
                return Ok(new ResponseOutcome<object>(data: new { ID.ID, Response = true, rowAffected }, status: HttpStatusCode.OK, message: "success delete club, this club is inactive now but you can see all previous information about it."));
            else if (rowAffected == 0)
                return Ok(new ResponseOutcome<object>(data: new { ID.ID, Response = false, rowAffected }, status: HttpStatusCode.OK, message: "no changes made. Club status is already inactive"));

            return StatusCode(
               StatusCodes.Status500InternalServerError,
                   new NullableResponseData(HttpStatusCode.InternalServerError, "An internal server error occurred. Please try again later.")
               );
        }
        catch (Exception)
        {
            return StatusCode(
            StatusCodes.Status500InternalServerError,
                new NullableResponseData(HttpStatusCode.InternalServerError, "An internal server error occurred. Please try again later.")
            );
        }
    }


    [HttpGet("GetAuthenticatedUserClubsHighlights")]
    [Authorize(Roles = nameof(Privileges.Owner))]
    public async Task<ActionResult<IEnumerable<ClubCoreDto>>> GetAuthenticatedUserClubsHighlights()
    {
        if (User == null)
            return Unauthorized();

        var userId = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;

        if (!int.TryParse(userId, out int userIdInt))
            return BadRequest("Invalid user ID format.");

        IEnumerable<ClubCoreDto> Clubs = await _Service.GetUserClubsHighlights(userIdInt);
        return Ok(new ResponseOutcome<IEnumerable<ClubCoreDto>>(data: Clubs, status: HttpStatusCode.OK, message: "this is you active clubs you own it."));
    }
}
