using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FilmSessions.Queries.GetList;

public class GetListFilmSessionQuery : IRequest<GetListResponse<GetListFilmSessionListItemDto>>,ISecuredRequest
{
    public string[] Roles => new string[] { "admin","user" };
    public PageRequest PageRequest { get; set; }

    public class GetListFilmSessionQueryHandler : IRequestHandler<GetListFilmSessionQuery, GetListResponse<GetListFilmSessionListItemDto>>
    {
        private readonly IFilmSessionRepository _filmSessionRepository;
        private readonly IMapper _mapper;

        public GetListFilmSessionQueryHandler(IFilmSessionRepository filmSessionRepository, IMapper mapper)
        {
            _filmSessionRepository = filmSessionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFilmSessionListItemDto>> Handle(GetListFilmSessionQuery request, CancellationToken cancellationToken)
        {
            Paginate<FilmSession> filmSessions = await _filmSessionRepository.GetListAsync(
                include: f => f.Include(f => f.Film).Include(f => f.Saloon),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize);
            GetListResponse<GetListFilmSessionListItemDto> response = _mapper.Map<GetListResponse<GetListFilmSessionListItemDto>>(filmSessions);
            return response;
        }
    }
}
