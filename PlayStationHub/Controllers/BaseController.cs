using Microsoft.AspNetCore.Mvc;
namespace PlayStationHub.API.Controllers;


public class BaseController<T> : ControllerBase
{
    protected readonly T _Service;
    public BaseController(T service)
    {
        _Service = service;
    }
}
