using Application.Features.Films.Queries.GetById;
using Application.Features.Films.Queries.GetList;
using Application.Features.FilmSessions.Queries.GetList;
using Application.Features.FilmSessions.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.Controllers
{
    public class UserFilmController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            GetListFilmQuery getListFilmQuery = new() { PageRequest = PageRequest };
            GetListResponse<GetListFilmListItemDto> response = await Mediator.Send(getListFilmQuery);

            return View(response);
        }
        public async Task<IActionResult> FilmDetail(Guid id, DateTime? date)
        {
            DateTime selectedDate = date ?? DateTime.Today; // Eğer tarih parametresi yoksa, bugünkü tarihi kullan

            var sort1 = new Sort("filmId", "asc");
            var filter1 = new Filter("filmSessionDate", "eq") { Value = selectedDate.ToString("yyyy-MM-dd") };       
            var filter = new Filter
            {
                Field = "filmId",
                Value = id.ToString(),
                Logic = "and",
                Operator = "eq",
                Filters = new List<Filter> { filter1 }
            };

            var dynamicQuery = new DynamicQuery(new List<Sort> { sort1 }, filter);

            GetListByDynamicFilmSessionQuery getListByDynamicFilmSessionQuery = new() { PageRequest = PageRequest, DynamicQuery = dynamicQuery };
            GetListResponse<GetListByDynamicFilmSessionListItemDto> response = await Mediator.Send(getListByDynamicFilmSessionQuery);

  
            return View(response);
        }
    }
}
