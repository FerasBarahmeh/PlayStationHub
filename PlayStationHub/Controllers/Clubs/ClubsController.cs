using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.Business.DataTransferObject.Clubs;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
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
}
