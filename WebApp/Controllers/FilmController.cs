using Application.Features.Films.Commands.Create;
using Application.Features.Films.Commands.Delete;
using Application.Features.Films.Commands.Update;
using Application.Features.Films.Queries.GetById;
using Application.Features.Films.Queries.GetList;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class FilmController : BaseController
    {
        private readonly FileUploadService _fileUploadService;

        public FilmController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListFilmQuery getListFilmQuery = new() { PageRequest = PageRequest };
            GetListResponse<GetListFilmListItemDto> response = await Mediator.Send(getListFilmQuery);
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateFilmCommand createFilmCommand, IFormFile image)
        {
            createFilmCommand.ImageUrl = await _fileUploadService.UploadFileAsync(image, "images");
            CreatedFilmResponse response = await Mediator.Send(createFilmCommand);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            GetByIdFilmQuery getByIdFilmQuery = new() { Id = id };
            GetByIdFilmResponse response = await Mediator.Send(getByIdFilmQuery);

            UpdateFilmCommand updateFilmCommand = new()
            {
                Id = response.Id,
                Name = response.Name,
            }; 

            return View(updateFilmCommand);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateFilmCommand updateFilmCommand)
        {
           
            UpdatedFilmResponse? response = await Mediator.Send(updateFilmCommand);
            return RedirectToAction("GetList");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteFilmCommand deleteFilmCommand = new() { Id = id };
            DeletedFilmResponse response = await Mediator.Send(deleteFilmCommand);

            return RedirectToAction("GetList");

        }
    }
}
