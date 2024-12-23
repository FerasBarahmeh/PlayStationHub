using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.DataTransferObject.Clubs;

namespace PlayStationHub.API.Controllers.Clubs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController(IClubService servic) : BaseController<IClubService>(servic)
    {
        [HttpGet]
        [Authorize(Roles = nameof(Privileges.Admin))]
        public async Task<ActionResult<ClubDTO>> All()
        {
            var clubs = await _Service.AllAsync();
            if (clubs == null) return NoContent();

            return Ok(clubs);
        }
    }
}
