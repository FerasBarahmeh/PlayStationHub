using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace PlayStationHub.API.Controllers;


public class BaseController<T> : ControllerBase
{
    protected readonly T _Service;
    protected readonly IMapper _Mapper;

    public BaseController(T service, IMapper mapper)
    {
        _Service = service;
        _Mapper = mapper;
    }
}
