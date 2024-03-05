using Application.Features.FilmSessions.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Responses;
using Application.Features.Films.Queries.GetList;
using Application.Features.FilmSessions.Commands.Create;
using Application.Features.FilmSessions.Queries.GetListByDynamic;
using Application.Features.FilmSessions.Commands.Update;
using Application.Features.FilmSessions.Queries.GetById;
using AutoMapper;
using Application.Features.Saloons.Queries.GetList;
using Application.Features.Saloons.Commands.Create;
using Application.Features.Films.Commands.Create;
using Application.Features.Saloons.Commands.Delete;
using Application.Features.FilmSessions.Commands.Delete;

namespace WebApp.Controllers;

public class FilmSessionController : BaseController
{
    IMapper _mapper;

    public FilmSessionController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<IActionResult> GetList()
    {
        GetListFilmSessionQuery getListFilmSessionQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListFilmSessionListItemDto> response = await Mediator.Send(getListFilmSessionQuery);
        return View(response);
    }
    public async Task<IActionResult> GetById(Guid id)
    {
        GetByIdFilmSessionQuery getByIdFilmSessionQuery = new();
        GetByIdFilmSessionResponse response = await Mediator.Send(getByIdFilmSessionQuery);
        return View(response);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        //Filmleri getiriyoruz
        GetListFilmQuery getListFilmQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListFilmListItemDto> filmResponse = await Mediator.Send(getListFilmQuery);
        ViewBag.Films = filmResponse;

        //Salonları getiriyoruz
        GetListSaloonQuery getListSaloonQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListSaloonListItemDto> saloonResponse = await Mediator.Send(getListSaloonQuery);
        ViewBag.Saloons = saloonResponse;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(CreateFilmSessionCommand createFilmSessionCommand)
    {
        CreatedFilmSessionResponse response = await Mediator.Send(createFilmSessionCommand);

        return RedirectToAction("GetList");
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        //Filmleri getiriyoruz
        GetListFilmQuery getListFilmQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListFilmListItemDto> filmResponse = await Mediator.Send(getListFilmQuery);
        ViewBag.Films = filmResponse;

        //Salonları getiriyoruz
        GetListSaloonQuery getListSaloonQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListSaloonListItemDto> saloonResponse = await Mediator.Send(getListSaloonQuery);
        ViewBag.Saloons = saloonResponse;

        //id'e göre filmSession ları getiriyoruz
        GetByIdFilmSessionQuery getByIdFilmSessionQuery = new() { Id = id };
        GetByIdFilmSessionResponse response = await Mediator.Send(getByIdFilmSessionQuery);

        UpdateFilmSessionCommand updateFilmSessionCommand = _mapper.Map<UpdateFilmSessionCommand>(response);

        return View(updateFilmSessionCommand);
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateFilmSessionCommand updateFilmSessionCommand)
    {

        UpdatedFilmSessionResponse response = await Mediator.Send(updateFilmSessionCommand);

        return RedirectToAction("GetList");
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        DeleteFilmSessionCommand deleteFilmSessionCommand = new() { Id = id };
        DeletedFilmSessionResponse response = await Mediator.Send(deleteFilmSessionCommand);

        return RedirectToAction("GetList");
    }

}
