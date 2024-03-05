using Application.Features.Films.Commands.Create;
using Application.Features.Films.Commands.Delete;
using Application.Features.Films.Commands.Update;
using Application.Features.Films.Queries.GetById;
using Application.Features.Films.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


public class FilmsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFilmCommand createFilmCommand)
    {
        CreatedFilmResponse response = await Mediator.Send(createFilmCommand);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFilmQuery getListFilmQuery = new() {PageRequest=pageRequest };
        GetListResponse<GetListFilmListItemDto> response =await Mediator.Send(getListFilmQuery);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFilmQuery getByIdFilmQuery = new() { Id = id};
        GetByIdFilmResponse response = await Mediator.Send(getByIdFilmQuery);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFilmCommand updateFilmCommand)
    {
        UpdatedFilmResponse response = await Mediator.Send(updateFilmCommand);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteFilmCommand deleteFilmCommand = new() { Id = id };
        DeletedFilmResponse response =await Mediator.Send(deleteFilmCommand);

        return Ok(response);

    }

}
