using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.DataTransferObject.Users.Requests;
using PlayStationHub.Business.Interfaces.Services;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Users;

[Route("/api/[controller]")]
[ApiController]
public class UsersController : BaseController<IUserService>
{
    public UsersController(IUserService service, IMapper mapper) : base(service, mapper) { }

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

    [HttpGet("Find/{ID}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Find(int ID)
    {
        var user = await _Service.FindAsync(ID);
        try
        {
            if (user == null) return NotFound(new NullableResponseData(HttpStatusCode.BadRequest, "Not found this user in our credentials"));
            return Ok(new ResponseOutcome<UserDTO>(data: user, HttpStatusCode.OK, message: "Success fetch user"));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            throw;
        }

    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Insert(InsertUserRequest user)
    {
        _Service.UserModel = _Mapper.Map<UserDTO>(user);
        _Service.Password = user.Password;

        var value = await _Service.SaveAsync();
        if (value)
            return Ok(new ResponseOutcome<int>(_Service.UserModel.ID, HttpStatusCode.Created, $"Success create new use has {_Service.UserModel.ID} identifier"));
        return StatusCode((int)HttpStatusCode.InternalServerError, "Some error according, try again later");
    }
}
