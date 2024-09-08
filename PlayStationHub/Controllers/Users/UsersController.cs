using Microsoft.AspNetCore.Mvc;
using PlayStationHub.Business.DataTransferObject;
using PlayStationHub.Business.Interfaces.Services;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Users;

[Route("/api/[controller]")]
[ApiController]
public class UsersController : BaseController<IUserService>
{
    public UsersController(IUserService service) : base(service) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> All()
    {
        var users = await _Service.AllAsync();
        if (users == null) return NoContent();

        return Ok(new ResponseOutcome<IEnumerable<UserDTO>>(
            users,
            System.Net.HttpStatusCode.OK,
            "Success fetch all users",
            new Utilities.Response.ThirdParty.Metadata()
            .Push("CountUsers", users.Count())
            ));
    }

}
