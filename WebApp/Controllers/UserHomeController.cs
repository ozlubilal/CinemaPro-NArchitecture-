using Application;
using Application.Features.FilmSessions.Queries.GetList;
using Application.Features.FilmSessions.Queries.GetListByDynamic;
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
      //  var sort2 = new Sort("FieldName2", "desc");

        DateTime now = DateTime.Now;
        TimeSpan timeOnly = now.GetTimeOnly();


        string nowString = now.ToString("yyyy-MM-dd");

        // Örnek filter nesnesi oluşturalım
        var filter1 = new Filter("startTime", "gt") { Value = timeOnly.ToString() };
        var filter2 = new Filter("filmSessionDate", "lt") { Value = nowString };
        ViewBag.time = timeOnly;
              

        // Filtrelerin birleştirilmesi
        var filter = new Filter
        {
            Field = "filmSessionDate",
            Value = nowString,
            Logic="and",
            Operator = "lt",
            Filters = new List<Filter> {filter1,filter2}
        };

        // DynamicQuery nesnesini oluşturalım
        var dynamicQuery = new DynamicQuery(new List<Sort> { sort1 }, filter);


        GetListByDynamicFilmSessionQuery getListByDynamicFilmSessionQuery = new() { PageRequest = PageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicFilmSessionListItemDto> response = await Mediator.Send(getListByDynamicFilmSessionQuery);
        return View(response);
    }
}
