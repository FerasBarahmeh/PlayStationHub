using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.Owners;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Owners;

[Route("api/[controller]")]
[ApiController]
public class OwnersController(IOwnerService service, IAuthService _AuthService) : BaseController<IOwnerService>(service)
{
    [HttpGet("GetOwnersCoreDetails")]
    [Authorize(Roles = nameof(Privileges.Admin))]
    public async Task<ActionResult<IEnumerable<OwnerCoreDetailsDto>>> GetOwnersCoreDetails()
    {
        IEnumerable<OwnerCoreDetailsDto> owners = await _Service.GetOwnersCoreDetailsAsync();
        if (owners == null) return NoContent();
        return Ok(new ResponseOutcome<IEnumerable<OwnerCoreDetailsDto>>(data: owners, HttpStatusCode.Created, "this is all owners"));
    }
}
