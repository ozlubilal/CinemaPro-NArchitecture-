using Application;
using Application.Features.Films.Queries.GetList;
using Application.Features.FilmSessions.Queries.GetById;
using Application.Features.FilmSessions.Queries.GetList;
using Application.Features.Tickets.Commands.Create;
using Application.Features.Tickets.Queries.GetList;
using Application.Features.Tickets.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
//using NuGet.Packaging;
using System;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace WebApp.Controllers
{
    public class UserTicketController : BaseController
    {
        IMapper _mapper;

        public UserTicketController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetList(GetListTicketListItemDto getListTicketListItemDto)
        {
            GetListTicketQuery getListTicketQuery = new() { PageRequest = PageRequest };
            GetListResponse<GetListTicketListItemDto> response = await Mediator.Send(getListTicketQuery);

            // Filtreleme işlemini başlatmadan önce orijinal liste yedeğini alalım
            var filteredItems = new List<GetListTicketListItemDto>(response.Items);

            // Parametre olarak gelen nesnenin özelliklerini tek tek kontrol edelim ve filtreleme yapalım
            if (!string.IsNullOrEmpty(getListTicketListItemDto.FirstName))
            {
                filteredItems = filteredItems.Where(item => item.FirstName == getListTicketListItemDto.FirstName).ToList();
            }

            if (!string.IsNullOrEmpty(getListTicketListItemDto.LastName))
            {
                filteredItems = filteredItems.Where(item => item.LastName == getListTicketListItemDto.LastName).ToList();
            }

            if (!string.IsNullOrEmpty(getListTicketListItemDto.FilmName))
            {
                filteredItems = filteredItems.Where(item => item.FilmName == getListTicketListItemDto.FilmName).ToList();
            }

            if (getListTicketListItemDto.FilmSessionDate != default)
            {
                filteredItems = filteredItems.Where(item => item.FilmSessionDate == getListTicketListItemDto.FilmSessionDate).ToList();
            }

            if (getListTicketListItemDto.StartTime != default)
            {
                filteredItems = filteredItems.Where(item => item.StartTime == getListTicketListItemDto.StartTime).ToList();
            }

            ViewBag.getListTicketDto = getListTicketListItemDto;


            // Sonuçları filtrelenmiş listeye atayalım
            response.Items = filteredItems;

            return View(response);
        }








        public async Task<IActionResult> GetList()
        {

            GetListTicketQuery query = new() { PageRequest = PageRequest };
            GetListResponse<GetListTicketListItemDto> response = await Mediator.Send(query);

            GetListFilmQuery getListFilmQuery = new() { PageRequest = PageRequest };
            GetListResponse<GetListFilmListItemDto> films = await Mediator.Send(getListFilmQuery);

            ViewBag.Films = films.Items;

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Add(Guid id)
        {
            GetByIdFilmSessionQuery getByIdFilmSessionQuery = new GetByIdFilmSessionQuery { Id = id };
            GetByIdFilmSessionResponse getByIdFilmSessionResponse = await Mediator.Send(getByIdFilmSessionQuery);

            // Örnek sort nesneleri oluşturalım
            var sort1 = new Sort("filmSessionId", "asc");
            // Filtrelerin birleştirilmesi
            var filter = new Filter
            {
                Field = "filmSessionId",
                Value = id.ToString(),
                Logic = "and",
                Operator = "eq",
                Filters = new List<Filter> { }
            };

            // DynamicQuery nesnesini oluşturalım
            var dynamicQuery = new DynamicQuery(new List<Sort> { sort1 }, filter);

            //filmSessionId'ye göre ticket verilerini getiriyoruz
            GetListByDynamicTicketQuery getListByDynamicTicketQuery = new() { DynamicQuery = dynamicQuery, PageRequest = PageRequest };
            GetListResponse<GetListByDynamicTicketListItemDto> response = await Mediator.Send(getListByDynamicTicketQuery);



            List<string> SelectedSeats = new List<string>();


            static List<string> CombineSeatNumbers(List<string> list)
            {
                List<string> combinedList = new List<string>();
                foreach (var item in list)
                {
                    combinedList.AddRange(item.Split(','));
                }
                return combinedList;
            }
            foreach (var ticketItem in response.Items)
            {
                SelectedSeats.AddRange(ticketItem.SelectedSeats);

            }
            List<string> combinedList = CombineSeatNumbers(SelectedSeats);

            ViewBag.NonSelectableSeats = combinedList;

            ViewBag.filmSession = getByIdFilmSessionResponse;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateTicketCommand createTicketCommand)
        {           
            createTicketCommand.SelectedSeats= createTicketCommand.SelectedSeats[0].Split(',').ToList();
            CreatedTicketResponse response = await Mediator.Send(createTicketCommand);
            return RedirectToAction("Index", "UserHome");
        }


    }
}
