using Application.Features.Saloons.Commands.Create;
using Application.Features.Saloons.Commands.Delete;
using Application.Features.Saloons.Commands.Update;
using Application.Features.Saloons.Queries.GetById;
using Application.Features.Saloons.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class SaloonController : BaseController
{
    public async Task<IActionResult> GetList()
    {
        GetListSaloonQuery getListSaloonQuery = new() { PageRequest = PageRequest };
        GetListResponse<GetListSaloonListItemDto> response = await Mediator.Send(getListSaloonQuery);

        return View(response);
    }
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    
    }
    [HttpPost]
    public async Task<IActionResult> Add(CreateSaloonCommand createSaloonCommand)
    {
        CreatedSaloonResponse response =await Mediator.Send(createSaloonCommand);

        return RedirectToAction("GetList");
    }
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        GetByIdSaloonQuery getByIdSaloonQuery = new() { Id = id };
        GetByIdSaloonReponse response =await Mediator.Send(getByIdSaloonQuery);

        UpdateSaloonCommand updateSaloonCommand = new() 
        {
            Id = response.Id,
            Name=response.Name,
        };

        return View(updateSaloonCommand);

    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateSaloonCommand updateSaloonCommand)
    {
        UpdatedSaloonResponse response = await Mediator.Send(updateSaloonCommand);

        return RedirectToAction("GetList");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
       DeleteSaloonCommand deleteSaloonCommand = new() { Id = id};
        DeletedSaloonResponse response = await Mediator.Send(deleteSaloonCommand);

        return RedirectToAction("GetList");
    }
}
