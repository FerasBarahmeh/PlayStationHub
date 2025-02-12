using Microsoft.AspNetCore.Mvc;
using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Requests.Clubs;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Clubs;

[Route("api/[controller]")]
[ApiController]
public class ClubsController(IClubService servic) : BaseController<IClubService>(servic)
{
    [HttpGet]

    public async Task<ActionResult> All()
    {
        var clubs = await _Service.AllAsync();
        if (clubs == null) return NoContent();

        return Ok(new ResponseOutcome<IEnumerable<ClubDTO>>(data: clubs, status: HttpStatusCode.OK, message: "Success fetch clubs"));
    }

    [HttpPost("Find")]
    public async Task<ActionResult> Find(FindClubRequest request)
    {
        var club = await _Service.FindAsync(request.ID);

        return Ok(new ResponseOutcome<ClubDTO>(data: club, status: HttpStatusCode.OK, message: "Success fetch clubs"));
    }
}
