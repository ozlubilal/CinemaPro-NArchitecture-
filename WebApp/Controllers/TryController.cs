using Application.Features.FilmSessions.Commands.Create;
using Application.Features.FilmSessions.Queries.GetById;
using Application.Features.FilmSessions.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Azure.Core;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class TryController : BaseController
    {
        IMapper _mapper;
        IFilmSessionRepository _filmSessionRepository;

        public TryController(IMapper mapper, IFilmSessionRepository filmSessionRepository)
        {
            _mapper = mapper;
            _filmSessionRepository = filmSessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Paginate<FilmSession> filmSessions = await _filmSessionRepository.GetListAsync(
            //      index: 0,
            //      size: 100);

            //for (int i = 1; i <26; i++)
            //foreach (var filmSession in filmSessions.Items)
            //{
            //    var x = filmSession;
            //    x.FilmSessionDate = DateTime.Now.AddDays(i);
            //    x.Id = new Guid();
            //    await _filmSessionRepository.AddAsync(x);
            //}
            
     
          


            return View();
        }
    }
}
