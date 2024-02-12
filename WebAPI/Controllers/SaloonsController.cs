using Application.Features.Saloons.Commands.Create;
using Application.Features.Saloons.Commands.Delete;
using Application.Features.Saloons.Commands.Update;
using Application.Features.Saloons.Queries.GetById;
using Application.Features.Saloons.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaloonsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSaloonQuery getListSaloonQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSaloonListItemDto> response = await Mediator.Send(getListSaloonQuery);
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSaloonQuery getByIdSaloonQuery = new() { Id = id };
        GetByIdSaloonReponse response=await Mediator.Send(getByIdSaloonQuery); 
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSaloonCommand createSaloonCommand)
    {
        CreatedSaloonResponse response=await Mediator.Send(createSaloonCommand); 
        
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSaloonCommand updateSaloonCommand)
    {
        UpdatedSaloonResponse response=await Mediator.Send(updateSaloonCommand); 

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteSaloonCommand deleteSaloonCommand = new() { Id = id };

        DeletedSaloonResponse response = await Mediator.Send(deleteSaloonCommand);

        return Ok(response);
    }
}
