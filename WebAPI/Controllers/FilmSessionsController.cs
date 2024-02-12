using Application.Features.FilmSessions.Queries.GetListByDynamic;
using Application.Features.FilmSessions.Commands.Create;
using Application.Features.FilmSessions.Commands.Delete;
using Application.Features.FilmSessions.Commands.Update;
using Application.Features.FilmSessions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmSessionsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFilmSessionCommand createFilmSessionCommand)
        {
            CreatedFilmSessionResponse response = await Mediator.Send(createFilmSessionCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFilmSessionQuery getListFilmSessionQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFilmSessionListItemDto> response = await Mediator.Send(getListFilmSessionQuery);
            return Ok(response);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
        {
            GetListByDynamicFilmSessionQuery getListByDynamicFilmSessionQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
            GetListResponse<GetListByDynamicFilmSessionListItemDto> response = await Mediator.Send(getListByDynamicFilmSessionQuery);
            return Ok(response);
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById([FromRoute] Guid id)
        //{
        //    GetByIdFilmSessionQuery getByIdFilmSessionQuery = new() { Id = id };
        //    GetByIdFilmSessionResponse response = await Mediator.Send(getByIdFilmSessionQuery);
        //    return Ok(response);
        //}

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFilmSessionCommand updateFilmSessionCommand)
        {
            UpdatedFilmSessionResponse response = await Mediator.Send(updateFilmSessionCommand);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeleteFilmSessionCommand deleteFilmSessionCommand = new() { Id = id };
            DeletedFilmSessionResponse response = await Mediator.Send(deleteFilmSessionCommand);

            return Ok(response);

        }
    }
}
