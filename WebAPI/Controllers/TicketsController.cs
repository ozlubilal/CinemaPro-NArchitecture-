using Application.Features.Tickets.Commands.Create;
using Application.Features.Tickets.Commands.Delete;
using Application.Features.Tickets.Commands.Update;
using Application.Features.Tickets.Queries.GetList;
using Application.Features.Tickets.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTicketCommand createTicketCommand)
    {
        CreatedTicketResponse response = await Mediator.Send(createTicketCommand);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTicketQuery getListTicketQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTicketListItemDto> response = await Mediator.Send(getListTicketQuery);
        return Ok(response);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetById([FromRoute] Guid id)
    //{
    //    GetByIdTicketQuery getByIdTicketQuery = new() { Id = id };
    //    GetByIdTicketResponse response = await Mediator.Send(getByIdTicketQuery);
    //    return Ok(response);
    //}
    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery)
    {
        GetListByDynamicTicketQuery getListByDynamicModelQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicTicketListItemDto> response = await Mediator.Send(getListByDynamicModelQuery);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTicketCommand updateTicketCommand)
    {
        UpdatedTicketResponse response = await Mediator.Send(updateTicketCommand);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteTicketCommand deleteTicketCommand = new() { Id = id };
        DeletedTicketResponse response = await Mediator.Send(deleteTicketCommand);

        return Ok(response);

    }
}
