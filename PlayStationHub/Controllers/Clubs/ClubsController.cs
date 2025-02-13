using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}
