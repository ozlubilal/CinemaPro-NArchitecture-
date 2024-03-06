using Application;
using Application.Features.FilmSessions.Queries.GetList;
using Application.Features.FilmSessions.Queries.GetListByDynamic;
using Application.Features.Saloons.Queries.GetById;
using Application.Features.Tickets.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.CodeAnalysis.Elfie.Model;

namespace WebApp.Controllers;

public class UserHomeController : BaseController
{
    public async Task<IActionResult> Index()
    {
        //GetListFilmSessionQuery getListFilmSessionQuery = new() { PageRequest = PageRequest };
        //GetListResponse<GetListFilmSessionListItemDto> response = await Mediator.Send(getListFilmSessionQuery);


        // Örnek sort nesneleri oluşturalım
        var sort1 = new Sort("filmSessionDate", "asc");
        var sort2 = new Sort("startTime", "asc");

        DateTime now = DateTime.Now;
        TimeSpan timeOnly = now.GetTimeOnly();


        string nowString = now.ToString("yyyy-MM-dd");

        // Örnek filter nesnesi oluşturalım
        var filter1 = new Filter("startTime", "gt") { Value = timeOnly.ToString() };
              

        // Filtrelerin birleştirilmesi
        var filter = new Filter
        {
            Field = "filmSessionDate",
            Value = nowString,
            Logic="and",
            Operator = "eq",
            Filters = new List<Filter> {filter1}
        };

        // DynamicQuery nesnesini oluşturalım
        var dynamicQuery = new DynamicQuery(new List<Sort> { sort2 }, filter);

      

        GetListByDynamicFilmSessionQuery getListByDynamicFilmSessionQuery = new() { PageRequest = PageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicFilmSessionListItemDto> response = await Mediator.Send(getListByDynamicFilmSessionQuery);

        ViewBag.FilmList = response.Items.Select(item=>item.FilmName).Distinct().ToList();
        ViewBag.FilmName = "Amcalar";
        return View(response);
    }
    [HttpPost]
    public async Task<IActionResult> Index(string filmName, string startTime)
    {
        ViewBag.FilmName = filmName;
        ViewBag.StartTime = startTime;
        var sort2 = new Sort("startTime", "asc");

        DateTime now = DateTime.Now;
        TimeSpan timeOnly = now.GetTimeOnly();
   
        string today =now.ToString("yyyy-MM-dd");
        // Örnek filter nesnesi oluşturalım
        var filter1 = new Filter("startTime", "gt") { Value = timeOnly.ToString() };

        // Filtrelerin birleştirilmesi
        var filter = new Filter
        {
            Field = "filmSessionDate",
            Value = today,
            Logic = "and",
            Operator = "eq",
            Filters = new List<Filter> { filter1 }
        };

        // DynamicQuery nesnesini oluşturalım
        var dynamicQuery = new DynamicQuery(new List<Sort> { sort2 }, filter);


        GetListByDynamicFilmSessionQuery getListByDynamicFilmSessionQuery = new() { PageRequest = PageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicFilmSessionListItemDto> response = await Mediator.Send(getListByDynamicFilmSessionQuery);

        ViewBag.FilmList = response.Items.Select(item => item.FilmName).Distinct().ToList();

        var filteredItems = new List<GetListByDynamicFilmSessionListItemDto>(response.Items);

            if(!string.IsNullOrEmpty(filmName))
        {
            filteredItems=filteredItems.Where(x=>x.FilmName== filmName).ToList();
        }
        if (!string.IsNullOrEmpty(startTime) && TimeSpan.TryParse(startTime, out TimeSpan startTimeSpan))
        {
            filteredItems = filteredItems.Where(x => x.StartTime >= startTimeSpan).ToList();
        }

        ViewBag.FilmName = "Amcalar";
        response.Items= filteredItems;


        return View(response);
    }
}
