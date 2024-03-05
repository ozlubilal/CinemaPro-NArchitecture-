using Application.Features.Films.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class FilmController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            pageRequest.PageIndex= 0;
            pageRequest.PageSize= 10;
            GetListFilmQuery getListFilmQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListFilmListItemDto> response = await Mediator.Send(getListFilmQuery);
            return View(response);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
