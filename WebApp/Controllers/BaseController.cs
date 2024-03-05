using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class BaseController : Controller
{
    private IMediator? _mediator;
    protected PageRequest PageRequest = new() { PageIndex=0,PageSize=100 };

    //daha önce mediatr varsa onu döndür yoksa injecte et onu döndür
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected string? GetIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }

}
