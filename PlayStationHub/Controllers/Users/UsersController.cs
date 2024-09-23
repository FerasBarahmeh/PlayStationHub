﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayStationHub.API.Authentication;
using PlayStationHub.API.Filters;
using PlayStationHub.Business.DataTransferObject.Users;
using PlayStationHub.Business.DataTransferObject.Users.Requests;
using PlayStationHub.Business.Enums;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Mappers;
using System.Net;
using Utilities.Response;

namespace PlayStationHub.API.Controllers.Users;

[Route("/api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : BaseController<IUserService>
{
    private readonly ClaimsHelper _ClaimsHelper;
    public UsersController(IUserService service, ClaimsHelper claimsHelper) : base(service)
    {
        _ClaimsHelper = claimsHelper;
    }

    [HttpGet]
    [Authorize(Roles = nameof(Privileges.Admin))]
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
    [Authorize(Roles = nameof(Privileges.Admin))]
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
        _Service.UserModel = UserMapper.ToUserDTO(user);
        _Service.Password = user.Password;

        var value = await _Service.SaveAsync();
        if (value)
            return Ok(new ResponseOutcome<object>(new { UserID = _Service.UserModel.ID }, HttpStatusCode.Created, $"Success create new use has {_Service.UserModel.ID} identifier"));
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

    [HttpPatch]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Update(UpdateUserRequest Request)
    {
        Request.ID = _ClaimsHelper.ID;
        if (!Request.ID.HasValue) return Unauthorized();

        var user = await _Service.FindAsync(Request.ID.Value);

        if (user == null) return NotFound(new NullableResponseData(HttpStatusCode.NotFound, "Not found user in our credentials"));

        user = UserMapper.MargeUserDtoWithUpdateRequest(Request, user);

        _Service.UserModel = user;


        bool IsUpdated = await _Service.SaveAsync();

        if (!IsUpdated)
            return StatusCode((int)HttpStatusCode.InternalServerError, "Occur error try again later pls.");

        return Ok(new ResponseOutcome<UserDTO>(_Service.UserModel, HttpStatusCode.OK, "Success Update you profile"));
    }
}
