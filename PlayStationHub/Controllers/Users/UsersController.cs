using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Authentication;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.DTOs.User;
using System.Net;
using Utilities.Response;
using Utilities.Response.interfaces;

namespace PlayStationHub.API.Controllers.Users;

[Route("/api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(Privileges.Admin))]
public class UsersController : BaseController<IUserService>
{
    private readonly ClaimsHelper _ClaimsHelper;
    private readonly IMapper _Mapper;

    public UsersController(IUserService service, ClaimsHelper claimsHelper, IMapper mapper) : base(service)
    {
        _ClaimsHelper = claimsHelper;
        _Mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = nameof(Privileges.Admin))]
    public async Task<ActionResult<IPagedResponse<UserDto>>> All(int pageNumber = 1, int pageSize = 10)
    {
        var users = await _Service.PagedTableAsync(pageNumber, pageSize);
        if (users == null) return NoContent();

        return Ok(new PagedResponse<UserDto>
        {
            Data = users,
            SlideNumber = pageNumber,
            SlideSize = pageSize,
            TotalCount = await _Service.CountRowsAsync(),
            StatusCode = HttpStatusCode.OK,
            Message = $"this is slide number {pageNumber}"
        });
    }

    [HttpGet("Find/{ID}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Find(int ID)
    {
        var user = await _Service.FindAsync(ID);
        try
        {
            if (user == null) return NotFound(new NullableResponseData(HttpStatusCode.BadRequest, "Not found this user in our credentials"));
            return Ok(new ResponseOutcome<UserDto>(data: user, HttpStatusCode.OK, message: "Success fetch user"));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            throw;
        }

    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Insert(InsertUserDto user)
    {
        _Service.User = _Mapper.Map<UserDto>(user);
        _Service.Password = user.Password;

        var value = await _Service.SaveAsync();
        if (value)
            return Ok(new ResponseOutcome<object>(new { UserID = _Service.User.ID }, HttpStatusCode.Created, $"Success create new use has {_Service.User.ID} identifier"));
        return StatusCode((int)HttpStatusCode.InternalServerError, "Some error according, try again later");
    }

    [HttpDelete("{ID}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Delete(int ID)
    {
        if (await _Service.DeleteAsync(ID))
            return Ok(new ResponseOutcome<bool>(data: true, status: HttpStatusCode.OK, message: $"Successfully deleted user with ID {ID}."));

        return StatusCode((int)HttpStatusCode.InternalServerError, "User not found or could not be deleted.");
    }

    [HttpPatch("Update")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [Authorize]
    public async Task<IActionResult> Update(UpdateUserDto Request)
    {
        Request.ID = Request.ID ?? _ClaimsHelper.ID;

        var user = await _Service.FindAsync(Request.ID ?? 0);

        if (user == null)
            return NotFound(new NullableResponseData(HttpStatusCode.NotFound, "Not found user in our credentials"));

        user = _Mapper.Map(Request, user);

        _Service.User = user;

        bool IsUpdated = await _Service.SaveAsync();

        if (!IsUpdated)
            return StatusCode((int)HttpStatusCode.InternalServerError, "Occur error try again later pls.");

        return Ok(new ResponseOutcome<UserDto>(_Service.User, HttpStatusCode.OK, "Success Update you profile"));
    }


    [HttpGet("Count")]
    public async Task<IActionResult> UsersCount()
    {
        return Ok(new ResponseOutcome<int>(await _Service.CountRowsAsync(), HttpStatusCode.OK, ""));
    }
}
